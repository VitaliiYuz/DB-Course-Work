using AutoMapper;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;

namespace TileShop.Application.Profiles;

public class RatingToDto : Profile
{
    public RatingToDto()
    {
        CreateMap<Rating, RatingDto>().ReverseMap();
    }
}
