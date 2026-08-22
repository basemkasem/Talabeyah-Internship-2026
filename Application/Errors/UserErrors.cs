using Application.Common;
using Application.Dtos;
using Domain.Models;

namespace Application.Errors;

public static class UserErrors
{
    public static Error Unauthorized() =>
        new(ErrorType.Unauthorized, $"{nameof(User)}.Unauthorized", "Invalid username or password.");

    public static Error UsernameAlreadyExists() =>
        Error.Conflict($"{nameof(UserDto.Username)}.UsernameAlreadyExists", "Username already exists.");

    public static Error UsernameExceedMaxLength() =>
        Error.Validation(nameof(UserDto.Username), "Username length can't be more than 100 characters");

    public static Error UsernameTooShort() =>
        Error.Validation(nameof(UserDto.Username), "Username length is too short.");

    public static Error InvalidPasswordInput() =>
        Error.Validation(nameof(UserDto.Password),
            "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
}