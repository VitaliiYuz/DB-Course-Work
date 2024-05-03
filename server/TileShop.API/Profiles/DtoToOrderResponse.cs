using AutoMapper;
using TileShop.API.Order.Responses;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class DtoToOrderResponse : Profile
{
    public DtoToOrderResponse()
    {
        CreateMap<OrderDto, OrderResponse>();
    }
}
