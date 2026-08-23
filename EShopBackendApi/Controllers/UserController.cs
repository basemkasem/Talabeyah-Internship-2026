using Application.Common;
using Application.Dtos;
using Application.Services;
using EShopBackendApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EShopBackendApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        var result = await userService.Register(userDto);
        return result.Match(
            token => Ok(token),
            error => error.ToActionResult(this)
        );
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserDto userDto)
    {
        var result = await userService.Login(userDto);
        return result.Match(
            token => Ok(token),
            error => error.ToActionResult(this)
        );
    }
}