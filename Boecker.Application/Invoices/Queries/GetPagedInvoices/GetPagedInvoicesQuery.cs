using Boecker.Application.Invoices.Dtos;
using Boecker.Domain.Constants;
using Boecker.Domain.Constants.Filters;
using Boecker.Domain.Constants.Pagination;
using MediatR;

namespace Boecker.Application.Invoices.Queries.GetPagedInvoices;

public class GetPagedInvoicesQuery : PaginationParams, IRequest<PaginatedResult<InvoiceDto>>
{
    public int? ClientId { get; set; }
    public bool? IsRecurring { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public InvoiceStatus? Status { get; set; }

}
