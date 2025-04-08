using Boecker.Application.Clients.Commands.CreateClients;
using Boecker.Application.Clients.Commands.DeleteClient;
using Boecker.Application.Clients.Commands.UpdateClient;
using Boecker.Application.Clients.Dtos;
using Boecker.Application.Clients.Queries.GetAllClients;
using Boecker.Application.Clients.Queries.GetClientById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ClientsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll([FromQuery] GetAllClientsQuery query)
    {
        var clients = await mediator.Send(query);
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto?>> GetById([FromRoute] int id)
    {
        var client = await mediator.Send(new GetClientByIdQuery(id));
        return Ok(client);
    }

    [HttpDelete("{id}")]
    //specifies the expected HTTP response status code to be not content or not found
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteClient([FromRoute] int id)
    {
        await mediator.Send(new DeleteClientCommand(id));

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(CreateClientCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClientCommand command)
    {
        if (id != command.ClientId)
            return BadRequest("Mismatched client ID.");

        await mediator.Send(command);
        return NoContent();
    }
}
