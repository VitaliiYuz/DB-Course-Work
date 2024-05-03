using AutoMapper;
using TileShop.API.Products.Responses;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class DtoToProductResponse : Profile
{
    public DtoToProductResponse()
    {
        CreateMap<ProductDto, ProductResponse>();
    }
}
