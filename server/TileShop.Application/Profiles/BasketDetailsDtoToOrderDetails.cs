using AutoMapper;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;

namespace TileShop.Application.Profiles;

public class BasketDetailsDtoToOrderDetails : Profile
{
    public BasketDetailsDtoToOrderDetails()
    {
        CreateMap<BasketDetailsDto, OrderDetails>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.UnitPrice, opt => opt.MapFrom(x => x.Product.Price))
            .ForMember(x => x.Product, opt => opt.Ignore());
    }
}
