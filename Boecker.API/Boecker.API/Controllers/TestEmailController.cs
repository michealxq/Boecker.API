using Boecker.Domain.Entities;
using Boecker.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestEmailController(IMediator mediator) : ControllerBase
{
    [HttpPost("payment")]
    public async Task<IActionResult> SendTestPaymentReminder()
    {
        var testInvoice = new Invoice
        {
            InvoiceId = 999,
            InvoiceNumber = "TEST-INV-999",
            DueDate = DateTime.UtcNow.AddDays(3),
            Client = new Client
            {
                ClientId = 123,
                Email = "test@example.com",  // Replace with your SMTP4DEV test email
                Name = "Test Client"
            }
        };

        await mediator.Publish(new PaymentReminderEvent(testInvoice));
        return Ok("✅ Test PaymentReminderEvent published.");
    }

    [HttpPost("service")]
    public async Task<IActionResult> SendTestServiceReminder()
    {
        await mediator.Publish(new UpcomingServiceReminderEvent(
            serviceScheduleId: 888,
            dateScheduled: DateTime.UtcNow.AddDays(2),
            clientEmail: "test@example.com"  // Replace with your SMTP4DEV test email
        ));

        return Ok("✅ Test UpcomingServiceReminderEvent published.");
    }
}
