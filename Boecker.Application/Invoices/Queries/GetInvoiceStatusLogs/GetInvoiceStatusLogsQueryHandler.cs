
using Boecker.Application.Invoices.Dtos;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Invoices.Queries.GetInvoiceStatusLogs;

public class GetInvoiceStatusLogsQueryHandler(
    IInvoiceRepository invoiceRepository)
    : IRequestHandler<GetInvoiceStatusLogsQuery, List<InvoiceStatusLogDto>>
{
    public async Task<List<InvoiceStatusLogDto>> Handle(GetInvoiceStatusLogsQuery request, CancellationToken cancellationToken)
    {
        var logs = await invoiceRepository.GetInvoiceStatusLogsAsync(request.InvoiceId);

        return logs.Select(x => new InvoiceStatusLogDto
        {
            OldStatus = x.OldStatus.ToString(),
            NewStatus = x.NewStatus.ToString(),
            StatusChangedOn = x.StatusChangedOn,
            ChangedBy = x.ChangedBy
        }).ToList();
    }
}


