
using Boecker.Domain.Entities;
using MediatR;

namespace Boecker.Domain.Events;

public class InvoiceCreatedEvent : INotification
{
    public Invoice Invoice { get; }

    public InvoiceCreatedEvent(Invoice invoice)
    {
        Invoice = invoice;
    }
}