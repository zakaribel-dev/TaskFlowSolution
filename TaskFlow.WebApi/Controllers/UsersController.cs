using Microsoft.AspNetCore.Mvc;
using TaskFlow.Domain.DTOs;
using TaskFlow.WebApi.Services;

namespace TaskFlow.WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        try
        {
            var user = _userService.Register(dto.Name, dto.Email, dto.Password);
            return Ok(new { user.Id, user.Name, user.Email });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        try
        {
            var token = _userService.Login(dto.Email, dto.Password);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
