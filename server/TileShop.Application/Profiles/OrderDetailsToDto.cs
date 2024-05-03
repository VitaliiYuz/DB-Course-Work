using AutoMapper;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;

namespace TileShop.Application.Profiles;

public class OrderDetailsToDto : Profile
{
    public OrderDetailsToDto()
    {
        CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
    }
}
