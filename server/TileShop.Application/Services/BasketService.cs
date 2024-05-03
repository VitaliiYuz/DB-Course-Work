using AutoMapper;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Application.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IBasketDetailsRepository _basketDetailsRepository;
    private readonly IMapper _mapper;

    public BasketService(IBasketRepository basketRepository, IBasketDetailsRepository basketDetailsRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _basketDetailsRepository = basketDetailsRepository;
        _mapper = mapper;
    }

    public async Task<BasketDto> GetUserBasketAsync(int userId)
    {
        var basket = await _basketRepository.GetByUserIdAsync(userId);
        return _mapper.Map<BasketDto>(basket);
    }

    public async Task<List<BasketDetailsDto>> GetBasketItemsAsync(int basketId)
    {
        var details = await _basketDetailsRepository.GetDetailsByBasketIdAsync(basketId);
        return _mapper.Map<List<BasketDetailsDto>>(details);
    }

    public async Task<BasketDto> CreateBasketAsync(int userId)
    {
        var userBasket = await _basketRepository.GetByUserIdAsync(userId);
        if(userBasket is not null)
        {
            throw new Exception("User basket already exist");
        }
        var basket = new Basket { UserId = userId };
        var created = await _basketRepository.CreateAsync(basket);
        return _mapper.Map<BasketDto>(created);
    }

    public async Task AddItemToBasketAsync(BasketDetailsDto basketItemDto)
    {
        var details = await _basketDetailsRepository.GetExistDetails(basketItemDto.BasketId, basketItemDto.ProductId);
        if(details is not null)
        {
            details.Quantity += 1;
            await _basketDetailsRepository.UpdateAsync(details);
            return;
        }
        var basketDetails = _mapper.Map<BasketDetails>(basketItemDto);
        await _basketDetailsRepository.CreateAsync(basketDetails);
    }

    public async Task UpdateBasketItemAsync(BasketDetailsDto basketItemDto)
    {
        var basketDetails = _mapper.Map<BasketDetails>(basketItemDto);
        await _basketDetailsRepository.UpdateAsync(basketDetails);
    }

    public async Task DeleteBasketItemAsync(int id)
    {
        await _basketDetailsRepository.DeleteAsync(id);
    }

    public async Task DeleteAllBasketItemsAsync(int basketId)
    {
        await _basketDetailsRepository.DeleteAllBasketItems(basketId);
    }
}
