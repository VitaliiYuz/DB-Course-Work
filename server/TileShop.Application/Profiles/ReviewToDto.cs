using AutoMapper;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;

namespace TileShop.Application.Profiles;

public class ReviewToDto : Profile
{
    public ReviewToDto()
    {
        CreateMap<Review, ReviewDto>().ReverseMap();
    }
}
