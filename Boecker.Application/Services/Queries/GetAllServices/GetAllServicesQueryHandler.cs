using AutoMapper;
using Boecker.Application.Invoices.Dtos;
using Boecker.Application.Invoices.Queries.GetAllInvoices;
using Boecker.Application.Services.Dtos;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Services.Queries.GetAllServices;

public class GetAllServicesQueryHandler(ILogger<GetAllServicesQueryHandler> logger,
    IMapper mapper,
    IServiceRepository serviceRepository) : IRequestHandler<GetAllServicesQuery, IEnumerable<ServiceDto>>
{
    public async Task<IEnumerable<ServiceDto>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Services ");
        var services = await serviceRepository.GetAllAsync();

        return mapper.Map<IEnumerable<ServiceDto>>(services);

    }
}
