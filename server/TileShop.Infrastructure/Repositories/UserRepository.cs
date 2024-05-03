using Microsoft.EntityFrameworkCore;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context)
        : base(context)
    {
        
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        var user = await EntitySet.FirstOrDefaultAsync(x => x.PhoneNumber == login || x.Email == login);

        return user;
    }

    public async Task<User> GetByLoginAndPasswordHashAsync(string login, string passwordHash)
    {
        var user = await EntitySet
            .Where(x => (x.PhoneNumber == login || x.Email == login) && x.PasswordHash == passwordHash)
            .FirstOrDefaultAsync();

        return user;
    }

    public bool IsUserExist(string phoneNumber, string email)
    {
        var isUserExist = EntitySet
            .Any(x => x.PhoneNumber == phoneNumber || x.Email == email);
        return isUserExist;
    }
}
