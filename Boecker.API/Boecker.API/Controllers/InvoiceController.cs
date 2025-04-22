using Boecker.Application.Invoices.Commands.CompleteInvoiceService;
using Boecker.Application.Invoices.Commands.CreateInvoices;
using Boecker.Application.Invoices.Commands.DeleteInvoice;
using Boecker.Application.Invoices.Commands.GenerateFinalInvoice;
using Boecker.Application.Invoices.Commands.GenerateProformaInvoice;
using Boecker.Application.Invoices.Commands.GenerateRecurringInvoices;
using Boecker.Application.Invoices.Commands.UpdateInvoice;
using Boecker.Application.Invoices.Commands.UpdateInvoiceServices;
using Boecker.Application.Invoices.Commands.UpdateInvoiceStatus;
using Boecker.Application.Invoices.Queries.DownloadInvoicePdf;
using Boecker.Application.Invoices.Queries.GetAllInvoices;
using Boecker.Application.Invoices.Queries.GetInvoiceById;
using Boecker.Application.Invoices.Queries.GetInvoicesByClientId;
using Boecker.Application.Invoices.Queries.GetInvoiceStatusLogs;
using Boecker.Application.Invoices.Queries.GetPagedInvoices;
using Boecker.Application.InvoiceServices.Queries.GetIncompleteServices;
using Boecker.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var invoice = await _mediator.Send(new GetInvoiceByIdQuery(id));
        return invoice is null ? NotFound() : Ok(invoice);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllInvoicesQuery());
        return Ok(result);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] InvoiceStatus status, InvoiceStatus newStatus)
    {
        var username = User.Identity?.Name; // gets the 'sub' or 'name' from JWT

        var command = new UpdateInvoiceStatusCommand
        {
            InvoiceId = id,
            Status = status,
            NewStatus = newStatus,
            ChangedBy = username 
        };

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/services")]
    public async Task<IActionResult> UpdateServices(
    int id,
    [FromBody] UpdateInvoiceServicesCommand command)
    {
        if (id != command.InvoiceId)
            return BadRequest("Invoice ID mismatch.");

        var updatedInvoice = await _mediator.Send(command);
        if (updatedInvoice is null)
            return NotFound();

        return Ok(updatedInvoice);
    }

    [HttpDelete("{id}")]
    
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteInvoiceCommand { InvoiceId = id });
        return NoContent();
    }

    [HttpGet("by-client/{clientId}")]
    public async Task<IActionResult> GetByClientId(int clientId)
    {
        var result = await _mediator.Send(new GetInvoicesByClientIdQuery(clientId));
        return Ok(result);
    }

    [HttpGet("{id}/pdf")]
    public async Task<IActionResult> DownloadPdf(int id)
    {
        var fileResult = await _mediator.Send(new DownloadInvoicePdfQuery(id));
        return fileResult;
    }

    [HttpPost("generate-recurring")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GenerateRecurring()
    {
        var count = await _mediator.Send(new GenerateRecurringInvoicesCommand());
        return Ok(new { generated = count });
    }

    [HttpPut("services/{id}/complete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CompleteService(int id, InvoiceStatus newStatus)
    {
        var username = User.Identity?.Name; // gets the 'sub' or 'name' from JWT

        await _mediator.Send(new CompleteInvoiceServiceCommand { InvoiceServiceId = id , NewStatus = newStatus , ChangedBy = username });
        return NoContent();
    }

    [HttpGet("services/incomplete/by-client/{clientId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetIncompleteServicesByClient(int clientId)
    {
        var result = await _mediator.Send(new GetIncompleteServicesQuery { ClientId = clientId });
        return Ok(result);
    }

    [HttpGet("{invoiceId}/status-logs")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetInvoiceLogs(int invoiceId)
    {
        var result = await _mediator.Send(new GetInvoiceStatusLogsQuery { InvoiceId = invoiceId });
        return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] GetPagedInvoicesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("final/{contractId}")]
    public async Task<IActionResult> GenerateFinalInvoice(int contractId, [FromQuery] decimal vat = 0)
    {
        var invoiceId = await _mediator.Send(new GenerateFinalInvoiceCommand(contractId, vat));

        return CreatedAtAction(
            actionName: nameof(GetById), 
            routeValues: new { id = invoiceId },
            value: new { invoiceId, message = "✅ Final invoice generated." }
        );
    }

    [HttpPost("proforma/{contractId}")]
    public async Task<IActionResult> GenerateProformaInvoice(int contractId, [FromQuery] decimal vat = 0)
    {
        var invoiceId = await _mediator.Send(new GenerateProformaInvoiceCommand(contractId, vat));

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = invoiceId },
            value: new { invoiceId, message = "✅ Proforma invoice generated." }
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInvoiceCommand command)
    {
        if (id != command.InvoiceId)
            return BadRequest("Invoice ID mismatch.");

        var result = await mediator.Send(command);
        return result ? Ok("✅ Invoice updated.") : NotFound("Invoice not found.");
    }


}
