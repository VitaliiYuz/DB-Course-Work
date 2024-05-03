using Microsoft.EntityFrameworkCore;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Infrastructure.Repositories;

public class OrderDetailsRepository : BaseRepository<OrderDetails>, IOrderDetailsRepository
{
    public OrderDetailsRepository(ApplicationDbContext context)
        : base(context)
    {
        
    }


    public async Task CreateRangeAsync(List<OrderDetails> orderDetails)
    {
        await EntitySet.AddRangeAsync(orderDetails);
        await Context.SaveChangesAsync();
    }
}
