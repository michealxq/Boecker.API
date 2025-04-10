
using Boecker.Application.Payments.Commands.CreatePayment;
using Boecker.Application.Payments.Commands.CreatePaymentByInvoiceNumber;
using Boecker.Application.Payments.Dtos;
using Boecker.Application.Payments.Queries.GetAllPayments;
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
        public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var id = await mediator.Send(command, cancellationToken);
            return Ok(new { message = "✅ Payment created", paymentId = id });
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
