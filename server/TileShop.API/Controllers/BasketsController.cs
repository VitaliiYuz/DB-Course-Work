using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileShop.API.Basket.Requests;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;

namespace TileShop.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BasketsController : BaseController
{
    private readonly IBasketService _basketService;
    private readonly IMapper _mapper;

    public BasketsController(IBasketService basketService, IMapper mapper)
    {
        _basketService = basketService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<BasketDto>> GetUserBasket()
    {
        var result = await _basketService.GetUserBasketAsync(int.Parse(UserId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBasket()
    {
        var result = await _basketService.CreateBasketAsync(int.Parse(UserId));
        return CreatedAtAction(nameof(CreateBasket), result);
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItemToBasket(AddItemRequest request)
    {
        var basketDetails = _mapper.Map<BasketDetailsDto>(request);
        await _basketService.AddItemToBasketAsync(basketDetails);
        return Ok();
    }

    [HttpPut("items")]
    public async Task<IActionResult> ChangeBasketItemQuantity(UpdateItemRequest request)
    {
        var basketDetails = _mapper.Map<BasketDetailsDto>(request);
        await _basketService.UpdateBasketItemAsync(basketDetails);
        return NoContent();
    }

    [HttpDelete("items/{id}")]
    public async Task<IActionResult> DeleteBasketItem(int id)
    {
        await _basketService.DeleteBasketItemAsync(id);
        return NoContent();
    }
}
