using AutoMapper;
using Boecker.Application.Invoices.Dtos;
using Boecker.Application.Invoices.Queries.GetInvoiceById;
using Boecker.Application.Services.Dtos;
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Services.Queries.GetServiceById;

public class GetServiceByIdQueryHandler(ILogger<GetServiceByIdQueryHandler> logger,
    IServiceRepository serviceRepository,
    IMapper mapper) : IRequestHandler<GetServiceByIdQuery, ServiceDto?>
{
    public async Task<ServiceDto?> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Service {ServiceId}", request.ServiceId);
        var service = await serviceRepository.GetByIdAsync(request.ServiceId)
                ?? throw new NotFoundException(nameof(Service), request.ServiceId.ToString());

        return service is null ? null : mapper.Map<ServiceDto>(service);
    }
}
