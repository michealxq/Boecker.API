using Boecker.Application.Payments.Commands.CreatePayment;
using Boecker.Application.Payments.Commands.DeletePayment;
using Boecker.Application.Payments.Queries.GetPaymentsByInvoiceId;
using Boecker.Domain.IRepositories;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeletePaymentCommand(id));
            return NoContent();
        }
    }
}
