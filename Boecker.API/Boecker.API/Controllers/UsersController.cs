using Boecker.Application.Users.Commands;
using Boecker.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // Only admins can view all users
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(new { UserId = userId });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.UserId)
            return BadRequest("User ID mismatch.");

        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/roles")]
    public async Task<IActionResult> AssignRoles(string id, [FromBody] AssignRolesCommand command)
    {
        if (id != command.UserId)
            return BadRequest("User ID mismatch.");

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteUserCommand { UserId = id });
        return NoContent();
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var user = await _mediator.Send(new GetCurrentUserQuery());
        return Ok(user);
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
