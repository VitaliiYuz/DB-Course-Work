using AutoMapper;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;

namespace TileShop.Application.Profiles;

public class OrderToDto : Profile
{
    public OrderToDto()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
    }
}
