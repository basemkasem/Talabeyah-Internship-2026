using System.Text.RegularExpressions;
using Application.Common;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService(IUserRepository userRepository, IAuthenticationService authenticationService) : IUserService
{
    public async Task<Result<string>> Register(UserDto userDto)
    {
        var dtoValidationResult = ValidateUserDto<string>(userDto);
        if (!dtoValidationResult?.IsSuccess ?? false)
            return dtoValidationResult;
        
        var nameExists = await userRepository.GetUser(userDto.Username);
        if (nameExists is not null)
            return UserErrors.UsernameAlreadyExists();

        var user = await userRepository.Add(userDto);
        var token = await authenticationService.CreateToken(user);
        return token;
    }

    public async Task<Result<string>> Login(UserDto userDto)
    {
        var userFromDb = await userRepository.GetUser(userDto.Username);
        if (userFromDb is null)
        {
            return UserErrors.Unauthorized();
        }

        var passwordIsValid = BCrypt.Net.BCrypt.Verify(userDto.Password, userFromDb.PasswordHashed);
        if (userDto.Username != userFromDb.Username || !passwordIsValid)
        {
            return UserErrors.Unauthorized();
        }

        var token = await authenticationService.CreateToken(userFromDb);
        return token;
    }

    private Result<T>? ValidateUserDto<T>(UserDto userDto)
    {
        if (userDto.Username.Length < 3)
            return UserErrors.UsernameTooShort();

        if (userDto.Username.Length > 100)
            return UserErrors.UsernameExceedMaxLength();
        
        if (!Regex.IsMatch(userDto.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"))
            return UserErrors.InvalidPasswordInput();
        
        return null;
    }
}