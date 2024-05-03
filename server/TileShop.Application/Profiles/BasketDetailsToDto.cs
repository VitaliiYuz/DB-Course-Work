using AutoMapper;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;

namespace TileShop.Application.Profiles;

public class BasketDetailsToDto : Profile
{
    public BasketDetailsToDto()
    {
        CreateMap<BasketDetails, BasketDetailsDto>().ReverseMap();
    }
}
