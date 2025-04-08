using AutoMapper;
using Boecker.Application.Invoices.Dtos;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Invoices.Queries.GetInvoicesByClientId;

public class GetInvoicesByClientIdQueryHandler(ILogger<GetInvoicesByClientIdQueryHandler> logger,
    IInvoiceRepository invoiceRepository,
    IMapper mapper) : IRequestHandler<GetInvoicesByClientIdQuery, IEnumerable<InvoiceDto>>
{
    public async Task<IEnumerable<InvoiceDto>> Handle(GetInvoicesByClientIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Invoice by client id{ClientId}", request.ClientId);
        var invoices = await invoiceRepository.GetInvoicesByClientIdAsync(request.ClientId);
        return mapper.Map<List<InvoiceDto>>(invoices);
    }
}
