
using Boecker.Domain.Constants;
using MediatR;

namespace Boecker.Application.Invoices.Commands.UpdateInvoiceStatus;



public class UpdateInvoiceStatusCommand : IRequest
{
    public int InvoiceId { get; set; }
    public InvoiceStatus Status { get; set; }
    public InvoiceStatus NewStatus { get; set; }

    public string? ChangedBy { get; set; } // ✅
}
