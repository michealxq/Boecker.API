
using Boecker.Domain.Constants;
using MediatR;

namespace Boecker.Application.Invoices.Commands.CompleteInvoiceService;

public class CompleteInvoiceServiceCommand : IRequest<Unit>
{
    public int InvoiceServiceId { get; set; }
    public InvoiceStatus NewStatus { get; set; }
    public string? ChangedBy { get; set; }
}
