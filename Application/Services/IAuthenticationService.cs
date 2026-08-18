using Domain.Models;

namespace Application.Services;

public interface IAuthenticationService
{
    Task<string> CreateToken(User user);
}