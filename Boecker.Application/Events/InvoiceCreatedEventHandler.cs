
using Boecker.Application.Common.Interfaces;
using Boecker.Domain.Events;
using MediatR;

namespace Boecker.Application.Events;

public class InvoiceCreatedEventHandler(
    IPdfService pdfService,
    IEmailService emailService) : INotificationHandler<InvoiceCreatedEvent>
{
    public async Task Handle(InvoiceCreatedEvent notification, CancellationToken cancellationToken)
    {
        var invoice = notification.Invoice;
        (string pathfile,byte[] pdfBytes )= pdfService.GenerateInvoicePdf(invoice);

        await emailService.SendEmailWithAttachmentAsync(
            invoice.Client.Email,
            $"Your Invoice {invoice.InvoiceNumber}",
            $"Dear {invoice.Client.Name},\n\nPlease find your invoice attached.",
            pdfBytes,
            $"Invoice_{invoice.InvoiceNumber}.pdf"
        );
    }
}

