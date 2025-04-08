using Boecker.Application.Services.Commands.CreateServices;
using Boecker.Application.Services.Commands.DeleteService;
using Boecker.Application.Services.Commands.UpdateService;
using Boecker.Application.Services.Queries.GetAllServices;
using Boecker.Application.Services.Queries.GetServiceById;
using Boecker.Application.Services.Queries.GetServicesByCategoryId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Boecker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateServicesCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllServicesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetServiceByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("by-category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        var result = await _mediator.Send(new GetServicesByCategoryIdQuery(categoryId));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteServiceCommand { Id = id });
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceCommand command)
    {
        if (id != command.ServiceId)
            return BadRequest("Mismatched service ID.");

        await _mediator.Send(command);
        return NoContent();
    }

}
