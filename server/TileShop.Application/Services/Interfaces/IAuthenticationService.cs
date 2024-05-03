using TileShop.Domain.Dtos;

namespace TileShop.Application.Services.Interfaces;

public interface IAuthenticationService
{
    string GetTokenString(UserDto userDto, TimeSpan expirationPeriod);
}
