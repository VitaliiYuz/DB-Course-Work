using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;
using TileShop.Domain.Repositories;

namespace TileShop.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    
    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetTokenString(UserDto userDto, TimeSpan expirationPeriod)
    {
        var encodedSecret = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"] ?? string.Empty);
        var secretKey = new SymmetricSecurityKey(encodedSecret);

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new(ClaimTypes.MobilePhone, userDto.PhoneNumber),
            new(ClaimTypes.Email, userDto.Email),
            new(ClaimTypes.Name, userDto.FirstName),
            new(ClaimTypes.Surname, userDto.LastName),
            new(ClaimTypes.Role, userDto.Role.ToString())
        };;

        var tokenOptions = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.Add(expirationPeriod),
            signingCredentials: signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}