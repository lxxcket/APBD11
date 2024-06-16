using APBD11.Entities;

namespace APBD11.Repositories;

public interface IUserRepository
{
    public Task<int> AddUser(User user);
    public Task<User> GetUserByLogin(string login);
    public Task<User> GetUserByRefreshToken(string refreshToken);

    public Task SaveChanges();
}