using AutoMapper;
using TileShop.API.Products.Requests;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class UpdateProductRequestToDto : Profile
{
    public UpdateProductRequestToDto()
    {
        CreateMap<UpdateProductRequest, ProductDto>();
    }
}
