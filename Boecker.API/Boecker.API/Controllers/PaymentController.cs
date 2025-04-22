
using Boecker.Application.Payments.Commands.CreatePayment;
using Boecker.Application.Payments.Commands.CreatePaymentByInvoiceNumber;
using Boecker.Application.Payments.Dtos;
using Boecker.Application.Payments.Queries.GetAllPayments;
using Boecker.Application.Payments.Queries.GetInvoicePaymentSummary;
using Boecker.Application.Payments.Queries.GetPaymentsByInvoiceId;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<PaymentResultDto>> Create(
            [FromBody] CreatePaymentCommand cmd,
            CancellationToken ct)
        {
            var result = await mediator.Send(cmd, ct);
            return CreatedAtAction(
                nameof(GetByInvoice),
                new { invoiceId = result.PaymentId },
                result);
        }

        [HttpGet("summary/{invoiceId}")]
        public async Task<ActionResult<PaymentSummaryDto>> GetSummary(int invoiceId)
        {
            var summary = await mediator.Send(new GetInvoicePaymentSummaryQuery(invoiceId));
            return Ok(summary);
        }



        [HttpGet("by-invoice/{invoiceId}")]
        public async Task<IActionResult> GetByInvoice(int invoiceId)
        {
            var result = await mediator.Send(new GetPaymentsByInvoiceIdQuery(invoiceId));
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments()
        {
            var result = await mediator.Send(new GetAllPaymentsQuery());
            return Ok(result);
        }

        [HttpPost("by-invoice-number")]
        public async Task<ActionResult<PaymentDto>> CreatePaymentByInvoiceNumber(CreatePaymentByInvoiceNumberCommand command, CancellationToken cancellationToken)
        {
            var paymentDto = await mediator.Send(command, cancellationToken);
            // Optionally, return 201 Created with location header.
            return CreatedAtAction(nameof(GetByInvoice), new { id = paymentDto.PaymentId }, paymentDto);
        }



    }
}
