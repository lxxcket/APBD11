using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APBD11.DTOs;
using APBD11.Entities;
using APBD11.Repositories;
using APBD11.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace APBD11.UseCases;

public class UserService : IUserService
{
     private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<int> RegisterUser(RegisterRequest model)
    {
        var hashedPasswordAndSalt = SecurityUtil.GetHashedPasswordAndSalt(model.Password);
        

        var user = new User()
        {
            Email = model.Email,
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityUtil.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };
        await _userRepository.AddUser(user);
        return 1;
    }

    public async Task<Object?> LoginUser(LoginRequest loginRequest)
    {
        User user = await _userRepository.GetUserByLogin(loginRequest.Login);

        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityUtil.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword)
        {
            return new UnauthorizedResult();
        }
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "rsherstiuk"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
            //Add additional data here
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5145",
            audience: "https://localhost:5145",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityUtil.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _userRepository.SaveChanges();
        return new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        };
    }

    public async Task<Object?> RefreshUserToken(RefreshTokenRequest tokenRequest)
    {
        var user = await _userRepository.GetUserByRefreshToken(tokenRequest.RefreshToken);
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }

        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "dnovo"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
            //Add additional data here
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: "https://localhost:5145",
            audience: "https://localhost:5145",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityUtil.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _userRepository.SaveChanges();

        return new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            refreshToken = user.RefreshToken
        };
    }
}