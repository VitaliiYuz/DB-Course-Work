using AutoMapper;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Application.Services;

public class OrderService : IOrderService
{
    private readonly IBasketService _basketService;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IOrderDetailsRepository _orderDetailsRepository;

    public OrderService(IBasketService basketService, IOrderRepository orderRepository, IMapper mapper, IOrderDetailsRepository orderDetailsRepository)
    {
        _basketService = basketService;
        _orderRepository = orderRepository;
        _mapper = mapper;
        _orderDetailsRepository = orderDetailsRepository;
    }

    public Dictionary<int, decimal> GetAverageOrderPrice()
    {
        var average = _orderRepository.GetAverageOrderForCurrentYearByMonth();
        return average;
    }

    public Dictionary<int, decimal> GetTotalOrder()
    {
        var total = _orderRepository.GetTotalOrderInCurrentYearForMonth();
        return total;
    }

    public Dictionary<int, int> GetOrderCountForCurrentYearByMonth()
    {
        var orders = _orderRepository.GetOrderCountForCurrentYearByMonth();
        return orders;
    }

    public async Task<List<OrderDto>> GetOrdersForLast30DaysAsync()
    {
        var orders = await _orderRepository.GetOrdersForLast30DaysAsync();
        return _mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<List<OrderDto>> GetUserOrdersAsync(int userId)
    {
        var orders = await _orderRepository.GetUserOrdersAsync(userId);
        return _mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<OrderDto> DoCheckoutAsync(int userId)
    {
        var basket = await _basketService.GetUserBasketAsync(userId);
        var basketDetails = await _basketService.GetBasketItemsAsync(basket.Id);
        if (!basketDetails.Any())
        {
            throw new Exception("Basket is empty");
        }
        var orderDetails = _mapper.Map<List<OrderDetails>>(basketDetails);
        var order = new Order
        {
            CreatedDate = DateTime.UtcNow,
            UserId = userId
        };
        var createdOrder = await _orderRepository.CreateAsync(order);
        orderDetails.ForEach(x => x.OrderId = createdOrder.Id);
        await _orderDetailsRepository.CreateRangeAsync(orderDetails);
        await _basketService.DeleteAllBasketItemsAsync(basket.Id);
        return _mapper.Map<OrderDto>(createdOrder);
    }
}
