using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Common;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthenticationService(JwtOptions jwtOptions) : IAuthenticationService
{
    public Task<string> CreateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = jwtOptions.Issuer,
            IssuedAt = DateTime.Now,
            Audience = jwtOptions.Audience,
            Expires = DateTime.Now.AddMinutes(jwtOptions.ExpireInMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.Username)
            })
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult(tokenHandler.WriteToken(token));
    }
}