using TileShop.Domain.Entities;

namespace TileShop.Domain.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product?> GetByIdWithChildAsync(int id);
    Task<List<Product>> GetByPriceRange(decimal minPrice, decimal maxPrice);
    Task<List<Product>> GetByPriceDescending();
    Task<List<Product>> GetByPriceIncreasing();
}
