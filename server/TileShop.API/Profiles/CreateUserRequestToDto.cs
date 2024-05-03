using AutoMapper;
using TileShop.API.Users.Requests;
using TileShop.Domain.Dtos;

namespace TileShop.API.Profiles;

public class CreateUserRequestToDto : Profile
{
    public CreateUserRequestToDto()
    {
        CreateMap<CreateUserRequest, UserDto>();
    }
}