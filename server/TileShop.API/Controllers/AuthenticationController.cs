using Microsoft.AspNetCore.Mvc;
using TileShop.API.Authentication.Requests;
using TileShop.API.Authentication.Responses;
using TileShop.Application.Services.Interfaces;

namespace TileShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
	private readonly IAuthenticationService _authenticationService;
	private readonly IUserService _userService;
	public readonly IConfiguration _configuration;

	public AuthenticationController(IAuthenticationService authenticationService, IUserService userService, IConfiguration configuration)
	{
		_authenticationService = authenticationService;
		_userService = userService;
		_configuration = configuration;
	}

	[HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var userDto = await _userService.GetByLoginAndPasswordAsync(request.Login, request.Password);

        if (userDto == null)
            return Unauthorized();

        var expirationPeriod = TimeSpan.FromMinutes(int.Parse(_configuration["Jwt:ExpirationPeriodInMinutes"]));
        var tokenString = _authenticationService.GetTokenString(userDto, expirationPeriod);
        return Ok(new LoginResponse { Token = tokenString });
    }
}

