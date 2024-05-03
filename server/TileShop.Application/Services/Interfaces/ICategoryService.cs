using TileShop.Domain.Dtos;

namespace TileShop.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto);
    Task UpdateCategoryAsync(CategoryDto categoryDto);
    Task DeleteCategoryAsync(int id);
}
