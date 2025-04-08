using Boecker.Application.Invoices.Dtos;
using MediatR;

namespace Boecker.Application.Invoices.Queries.GetAllInvoices;

public record GetAllInvoicesQuery : IRequest<IEnumerable<InvoiceDto>>;

