using AutoMapper;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;

namespace TileShop.Application.Profiles;

public class ProductToDto : Profile
{
    public ProductToDto()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
