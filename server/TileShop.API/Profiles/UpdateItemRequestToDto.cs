using AutoMapper;
using TileShop.API.Basket.Requests;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class UpdateItemRequestToDto : Profile
{
    public UpdateItemRequestToDto()
    {
        CreateMap<UpdateItemRequest, BasketDetailsDto>();
    }
}
