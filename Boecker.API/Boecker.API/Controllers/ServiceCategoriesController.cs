using Boecker.Application.Invoices.Queries.GetInvoiceById;
using Boecker.Application.ServiceCategories.Commands.CreateServiceCategories;
using Boecker.Application.ServiceCategories.Commands.DeleteServiceCategory;
using Boecker.Application.ServiceCategories.Commands.UpdateServiceCategory;
using Boecker.Application.ServiceCategories.Dtos;
using Boecker.Application.ServiceCategories.Queries.GetAllServiceCategories;
using Boecker.Application.ServiceCategories.Queries.GetServiceCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceCategoriesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceCategoriesCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllServiceCategoriesCommand());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetServiceCategoryByIdCommand(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteServiceCategoryCommand { Id = id });
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceCategoryCommand command)
    {
        if (id != command.Category.ServiceCategoryId)
            return BadRequest("Mismatched ID");

        await _mediator.Send(command);
        return NoContent();
    }
}

