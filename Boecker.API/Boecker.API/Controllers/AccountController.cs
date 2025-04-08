
using Boecker.Application.Auth.Dtos;
using Boecker.Application.Auth.Login.Commands;
using Boecker.Application.Auth.Register.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await mediator.Send(new LoginCommand(dto.Email, dto.Password));
        return Ok(new { token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var token = await mediator.Send(new RegisterCommand(dto.Email, dto.Password, dto.Role));
        return Ok(new { token });
    }
}
