
using Boecker.Application.Common.Interfaces;
using Boecker.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Events;

public class PaymentReminderEventHandler(
    IEmailService emailService,
    ILogger<PaymentReminderEventHandler> logger)
    : INotificationHandler<PaymentReminderEvent>
{
    public async Task Handle(PaymentReminderEvent notification, CancellationToken cancellationToken)
    {
        var invoice = notification.Invoice;

        if (invoice.Client == null)
        {
            logger.LogWarning("Invoice #{InvoiceId} has no client assigned. Skipping reminder.", invoice.InvoiceId);
            return;
        }

        var to = invoice.Client.Email;
        var subject = $"💳 Payment Reminder – Invoice #{invoice.InvoiceNumber}";
        var body = $"""
            <p>Dear {invoice.Client.Name},</p>
            <p>This is a friendly reminder that your invoice <strong>#{invoice.InvoiceNumber}</strong> is due on <strong>{invoice.DueDate:yyyy-MM-dd}</strong>.</p>
            <p>Total Amount: <strong>{invoice.TotalAfterTax:C}</strong></p>
            <p>Please make your payment on time to avoid any disruptions in service.</p>
            <p>Best regards,<br/>Boecker Pest Control Team</p>
        """;

        await emailService.SendEmailAsync(to, subject, body);
        logger.LogInformation("📧 Payment reminder sent to {Email} for invoice #{InvoiceId}", to, invoice.InvoiceId);
    }
}
