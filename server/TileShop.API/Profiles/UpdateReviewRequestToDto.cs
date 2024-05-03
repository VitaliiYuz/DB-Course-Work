using AutoMapper;
using TileShop.API.Reviews.Requests;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class UpdateReviewRequestToDto : Profile
{
    public UpdateReviewRequestToDto()
    {
        CreateMap<UpdateReviewRequest, ReviewDto>();
    }
}
