using AutoMapper;
using TileShop.API.Order.Responses;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class DtoToOrderDetailsResponse : Profile
{
    public DtoToOrderDetailsResponse()
    {
        CreateMap<OrderDetailsDto, OrderDetailsResponse>();
    }
}
