
using Boecker.Application.Invoices.Commands.CreateInvoices;
using MediatR;

namespace Boecker.Application.Contracts.Commands;

public class CreateContractCommand : IRequest<int>
{
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IncludesFollowUp { get; set; }
    public decimal VATPercentage { get; set; }

    public List<CreateInvoiceServiceDto> Services { get; set; } = new();
}
