using Microsoft.EntityFrameworkCore;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Infrastructure.Repositories;

public class BasketDetailsRepository : BaseRepository<BasketDetails>, IBasketDetailsRepository
{
    public BasketDetailsRepository(ApplicationDbContext context)
        : base(context)
    {
        
    }

    public async Task<BasketDetails?> GetExistDetails(int basketId, int productId)
    {
        var details = await EntitySet.FirstOrDefaultAsync(x => x.BasketId == basketId && x.ProductId == productId);
        return details;
    }

    public async Task<List<BasketDetails>> GetDetailsByBasketIdAsync(int id)
    {
        var basketDetails = await EntitySet
            .Where(x => x.BasketId == id)
            .Include(x => x.Product)
            .AsNoTracking()
            .ToListAsync();

        return basketDetails;
    }

    public async Task DeleteAllBasketItems(int id)
    {
        var basketDetails = await EntitySet
            .Where(x => x.BasketId == id)
            .ToListAsync();

        EntitySet.RemoveRange(basketDetails);
        await Context.SaveChangesAsync();
    }
}
