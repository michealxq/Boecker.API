
using Boecker.Application.Invoices.Dtos;
using MediatR;

namespace Boecker.Application.Invoices.Queries.GetInvoicesByClientId;

public record GetInvoicesByClientIdQuery(int ClientId) : IRequest<IEnumerable<InvoiceDto>>;
