using Application.Dtos;
using Application.Services;
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
        var register = await userService.Register(userDto);
        if (register.IsSuccess)
        {
            return Ok(register.Data);
        }
        if (register.ReturnType == ReturnType.NotFound)
        {
            return NotFound(register.Error);
        }
        return BadRequest(register.Error);
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserDto userDto)
    {
        var result = await userService.Login(userDto);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }

        return NotFound(result.Error);
    }
}