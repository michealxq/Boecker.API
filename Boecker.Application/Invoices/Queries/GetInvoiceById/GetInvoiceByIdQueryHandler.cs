using AutoMapper;
using Boecker.Application.Invoices.Dtos;
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Invoices.Queries.GetInvoiceById;

public class GetInvoiceByIdQueryHandler(ILogger<GetInvoiceByIdQueryHandler> logger,
    IInvoiceRepository invoiceRepository,
    IMapper mapper) : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto?>
{
    public async Task<InvoiceDto?> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Invoice {InvoiceId}", request.InvoiceId);
        var invoice = await invoiceRepository.GetByIdAsync(request.InvoiceId)
                ?? throw new NotFoundException(nameof(Invoice), request.InvoiceId.ToString());

        return invoice is null ? null : mapper.Map<InvoiceDto>(invoice);
    }
}
