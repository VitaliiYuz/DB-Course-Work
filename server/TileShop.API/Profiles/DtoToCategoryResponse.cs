using AutoMapper;
using TileShop.API.Categories.Responses;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class DtoToCategoryResponse : Profile
{
    public DtoToCategoryResponse()
    {
        CreateMap<CategoryDto, CategoryResponse>();
    }
}
