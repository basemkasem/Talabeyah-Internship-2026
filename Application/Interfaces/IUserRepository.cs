using Application.Dtos;
using Domain.Models;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<User>  Add(UserDto userDto);
    Task<User?> GetUser(string username);
}