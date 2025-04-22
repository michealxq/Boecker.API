

using Boecker.Application.Invoices.Dtos;
using MediatR;

namespace Boecker.Application.Invoices.Commands.UpdateInvoiceServices;

public class UpdateInvoiceServicesCommand : IRequest<InvoiceDto>
{
    public int InvoiceId { get; set; }
    public List<int> ServiceIds { get; set; } = new();
}
