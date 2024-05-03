using TileShop.Domain.Dtos;

namespace TileShop.Application.Services.Interfaces;

public interface IBasketService
{
    Task<BasketDto> GetUserBasketAsync(int userId);
    Task<List<BasketDetailsDto>> GetBasketItemsAsync(int basketId);
    Task<BasketDto> CreateBasketAsync(int userId);
    Task AddItemToBasketAsync(BasketDetailsDto basketItemDto);
    Task UpdateBasketItemAsync(BasketDetailsDto basketItemDto);
    Task DeleteBasketItemAsync(int id);
    Task DeleteAllBasketItemsAsync(int basketId);
}
