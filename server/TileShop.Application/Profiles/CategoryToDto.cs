using AutoMapper;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;

namespace TileShop.Application.Profiles;

public class CategoryToDto : Profile
{
    public CategoryToDto()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}
