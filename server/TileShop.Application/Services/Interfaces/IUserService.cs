using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;

namespace TileShop.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateAsync(UserDto userDto);
    Task<UserDto> GetByLoginAndPasswordAsync(string login, string password);
    Task<UserDto> GetUserByIdAsync(int id);
    Task UpdateAsync(UserDto userDto);
}
