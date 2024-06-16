using APBD11.DTOs;

namespace APBD11.UseCases;

public interface IUserService
{
    public Task<int> RegisterUser(RegisterRequest model);
    public Task<object?> LoginUser(LoginRequest loginRequest);
    public Task<object?> RefreshUserToken(RefreshTokenRequest tokenRequest);
}