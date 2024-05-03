using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;
using TileShop.Domain.Entities;
using TileShop.Domain.Repositories;

namespace TileShop.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateAsync(UserDto userDto)
    {
        if (_userRepository.IsUserExist(userDto.PhoneNumber, userDto.Email))
        {
            throw new Exception("User exist");
        }
        var user = _mapper.Map<User>(userDto);
        user.PasswordHash = ComputePasswordHash(userDto.Password, userDto.PasswordSalt);
        var createdUser = await _userRepository.CreateAsync(user);
        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task<UserDto> GetByLoginAndPasswordAsync(string login, string password)
    {
        var user = await _userRepository.GetByLoginAsync(login);
        if(user == null)
        {
            throw new Exception();
        }
        var passwordHash = ComputePasswordHash(password, user.PasswordSalt);
        var result = await _userRepository.GetByLoginAndPasswordHashAsync(login, passwordHash);
        if (result == null)
            throw new Exception("User not found");

        return _mapper.Map<UserDto>(result);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("User not found");
        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.PasswordHash = ComputePasswordHash(userDto.Password, user.PasswordSalt);
        await _userRepository.UpdateAsync(user);
    }

    private string ComputePasswordHash(string password, string salt)
    {
        var json = JsonConvert.SerializeObject(password + salt);
        var bytes = Encoding.UTF8.GetBytes(json);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}
