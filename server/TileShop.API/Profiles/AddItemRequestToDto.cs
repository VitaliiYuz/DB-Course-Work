using AutoMapper;
using TileShop.API.Basket.Requests;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class AddItemRequestToDto : Profile
{
    public AddItemRequestToDto()
    {
        CreateMap<AddItemRequest, BasketDetailsDto>();
    }
}
