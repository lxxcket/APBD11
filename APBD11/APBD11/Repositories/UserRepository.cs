using APBD11.Contexts;
using APBD11.Entities;
using Microsoft.EntityFrameworkCore;

namespace APBD11.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MedicamentContext _context;

    public UserRepository(MedicamentContext context)
    {
        _context = context;
    }

    public async Task<int> AddUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return 1;
    }

    public async Task<User> GetUserByLogin(string login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        return user;
    }

    public async Task<User> GetUserByRefreshToken(string refreshToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        return user;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}