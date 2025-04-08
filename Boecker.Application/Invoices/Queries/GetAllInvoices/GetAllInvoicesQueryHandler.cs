using AutoMapper;
using Boecker.Application.Invoices.Dtos;
using Boecker.Application.ServiceCategories.Dtos;
using Boecker.Application.ServiceCategories.Queries.GetAllServiceCategories;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Invoices.Queries.GetAllInvoices;

public class GetAllInvoicesQueryHandler(ILogger<GetAllInvoicesQueryHandler> logger,
    IMapper mapper,
    IInvoiceRepository invoiceRepository) : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceDto>>
{
    public async Task<IEnumerable<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Invoices ");
        var invoices = await invoiceRepository.GetAllAsync();

        return mapper.Map<IEnumerable<InvoiceDto>>(invoices);

    }
}
