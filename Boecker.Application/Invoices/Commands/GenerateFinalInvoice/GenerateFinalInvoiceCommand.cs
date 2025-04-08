
using MediatR;

namespace Boecker.Application.Invoices.Commands.GenerateFinalInvoice;

public record GenerateFinalInvoiceCommand(int ContractId, decimal VATPercentage) : IRequest<int>;
