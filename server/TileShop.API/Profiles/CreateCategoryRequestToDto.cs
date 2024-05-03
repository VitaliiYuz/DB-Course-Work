using AutoMapper;
using TileShop.API.Categories.Requests;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class CreateCategoryRequestToDto : Profile
{
    public CreateCategoryRequestToDto()
    {
        CreateMap<CreateCategoryRequest, CategoryDto>();
    }
}
