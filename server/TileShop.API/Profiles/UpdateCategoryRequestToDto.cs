using AutoMapper;
using TileShop.API.Categories.Requests;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class UpdateCategoryRequestToDto : Profile
{
    public UpdateCategoryRequestToDto()
    {
        CreateMap<UpdateCategoryRequest, CategoryDto>();
    }
}
