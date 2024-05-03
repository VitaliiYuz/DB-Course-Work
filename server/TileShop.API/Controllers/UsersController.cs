using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TileShop.API.Users.Requests;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;

namespace TileShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUserProfile()
    {
        if(UserId is null)
            return Unauthorized();
        var user = await _userService.GetUserByIdAsync(int.Parse(UserId));
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var userDto = _mapper.Map<UserDto>(request);
        var createdUser = await _userService.CreateAsync(userDto);
        return CreatedAtAction(nameof(CreateUser), createdUser);
    }

    [Authorize]
    [HttpPut]
    public void UpdateUser(UpdateUserRequest request)
    {
        var userDto = _mapper.Map<UserDto>(request);
        _userService.UpdateAsync(userDto);
    }
}
