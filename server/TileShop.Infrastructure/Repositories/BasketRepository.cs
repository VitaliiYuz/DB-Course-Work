using Microsoft.EntityFrameworkCore;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Infrastructure.Repositories
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        public BasketRepository(ApplicationDbContext context)
            :base(context)
        {
            
        }

        public async Task<Basket?> GetByUserIdAsync(int userId)
        {
            var basket = await EntitySet
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            return basket;
        }
    }
}
