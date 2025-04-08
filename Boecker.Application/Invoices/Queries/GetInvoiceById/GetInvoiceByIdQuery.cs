using Boecker.Application.Invoices.Dtos;
using MediatR;

namespace Boecker.Application.Invoices.Queries.GetInvoiceById;

public record GetInvoiceByIdQuery(int InvoiceId) : IRequest<InvoiceDto?>;
