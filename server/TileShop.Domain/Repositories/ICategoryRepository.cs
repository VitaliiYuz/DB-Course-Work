using TileShop.Domain.Entities;

namespace TileShop.Domain.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<List<Category>> GetCategoriesAsync();
}
