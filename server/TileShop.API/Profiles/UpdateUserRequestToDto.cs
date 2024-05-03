using AutoMapper;
using TileShop.API.Users.Requests;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class UpdateUserRequestToDto : Profile
{
    public UpdateUserRequestToDto()
    {
        CreateMap<UpdateUserRequest, UserDto>();
    }
}