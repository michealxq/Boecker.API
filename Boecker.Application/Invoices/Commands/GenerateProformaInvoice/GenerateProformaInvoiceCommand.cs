
using MediatR;

namespace Boecker.Application.Invoices.Commands.GenerateProformaInvoice;

public record GenerateProformaInvoiceCommand(int ContractId, decimal VATPercentage) : IRequest<int>;

