using TileShop.Domain.Entities;

namespace TileShop.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByLoginAsync(string login);
    Task<User> GetByLoginAndPasswordHashAsync(string login, string passwordHash);
    bool IsUserExist(string phoneNumber, string email);
}
