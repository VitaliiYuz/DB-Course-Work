using AutoMapper;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;

namespace TileShop.Application.Profiles;

public class BasketToDto : Profile
{
    public BasketToDto()
    {
        CreateMap<Basket, BasketDto>().ReverseMap();
    }
}
