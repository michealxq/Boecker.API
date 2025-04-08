
using AutoMapper;
using Boecker.Application.InvoiceServices.Dtos;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.InvoiceServices.Queries.GetIncompleteServices;

public class GetIncompleteServicesQueryHandler(
    IInvoiceRepository invoiceRepository)
    : IRequestHandler<GetIncompleteServicesQuery, List<IncompleteServiceDto>>
{
    public async Task<List<IncompleteServiceDto>> Handle(GetIncompleteServicesQuery request, CancellationToken cancellationToken)
    {
        var incomplete = await invoiceRepository.GetIncompleteInvoiceServicesByClientIdAsync(request.ClientId);

        return incomplete.Select(s => new IncompleteServiceDto
        {
            InvoiceServiceId = s.InvoiceServiceId,
            InvoiceNumber = s.Invoice.InvoiceNumber,
            ClientName = s.Invoice.Client.Name,
            ServiceName = s.Service.Name,
            CategoryName = s.Service.ServiceCategory?.Name,
            Completed = s.Completed,
            CompletionDate = s.CompletionDate
        }).ToList();
    }
}


