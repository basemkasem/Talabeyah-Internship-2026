using Application.Common;
using Application.Dtos;

namespace Application.Services;

public interface IUserService
{
    Task<Result<string>> Register(UserDto userDto);
    Task<Result<string>> Login(UserDto userDto);
}