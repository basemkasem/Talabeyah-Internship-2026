using System.Text.RegularExpressions;
using Application.Dtos;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService (IUserRepository userRepository, IAuthenticationService authenticationService) : IUserService
{
    public async Task<Result<string>> Register(UserDto userDto)
    {
        if (userDto.Username.Length < 3)
        {
            return Result<string>.Fail("Username length is too short.");
        }
        
        var nameExists = await userRepository.GetUser(userDto.Username);
        if (nameExists is not null)
        {
            return Result<string>.Fail("Username already exists.");
        }

        var validPassword = Regex.IsMatch(userDto.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");

        if (!validPassword)
        {
            return Result<string>.Fail("Invalid format for password.");
        }
        var user = await userRepository.Add(userDto);
        var token = await authenticationService.CreateToken(user);
        return Result<string>.Success(token);
    }

    public async Task<Result<string>> Login(UserDto userDto)
    {
        var userFromDb = await userRepository.GetUser(userDto.Username);
        if (userFromDb is null)
        {
            return Result<string>.NotFound("Invalid username or password.");
        }

        var passwordIsValid = BCrypt.Net.BCrypt.Verify(userDto.Password, userFromDb.PasswordHashed);
        if (userDto.Username == userFromDb.Username && passwordIsValid)
        {
            var token = await authenticationService.CreateToken(userFromDb);
            return Result<string>.Success(token);
        }

        return Result<string>.NotFound("Invalid username or password.");
    }
}