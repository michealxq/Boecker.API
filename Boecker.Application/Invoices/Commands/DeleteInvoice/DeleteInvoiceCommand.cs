
using MediatR;

namespace Boecker.Application.Invoices.Commands.DeleteInvoice;

public class DeleteInvoiceCommand : IRequest<Unit>
{
    public int InvoiceId { get; set; }
}
