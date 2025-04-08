
using Boecker.Application.Invoices.Dtos;
using MediatR;

namespace Boecker.Application.Invoices.Queries.GetInvoiceStatusLogs;

public class GetInvoiceStatusLogsQuery : IRequest<List<InvoiceStatusLogDto>>
{
    public int InvoiceId { get; set; }
}
