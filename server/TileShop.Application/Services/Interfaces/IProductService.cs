using TileShop.Domain.Dtos;

namespace TileShop.Application.Services.Interfaces;

public interface IProductService
{
    Task<ProductDto> GetByIdAsync(int id);
    Task<List<ProductDto>> GetProducts();
    Task<ProductDto> CreateProductAsync(ProductDto productDto);
    Task UpdateProductAsync(ProductDto productDto);
    Task DeleteProductAsync(int id);
}
