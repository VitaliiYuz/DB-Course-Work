using AutoMapper;
using TileShop.API.Products.Requests;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class CreateProductRequestToDto : Profile
{
    public CreateProductRequestToDto()
    {
        CreateMap<CreateProductRequest, ProductDto>();
    }
}
