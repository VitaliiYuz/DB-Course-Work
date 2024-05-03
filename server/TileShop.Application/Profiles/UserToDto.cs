using AutoMapper;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;

namespace TileShop.Application.Profiles;

public class UserToDto : Profile
{
    public UserToDto()
    {
        CreateMap<User, UserDto>()
            .ForMember(x => x.Password, opt => opt.Ignore())
            .ReverseMap();
    }
}
