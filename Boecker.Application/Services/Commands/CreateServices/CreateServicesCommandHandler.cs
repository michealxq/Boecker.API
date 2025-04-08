using AutoMapper;
using Boecker.Application.Invoices.Commands.CreateInvoices;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Services.Commands.CreateServices;

public class CreateServicesCommandHandler(ILogger<CreateServicesCommandHandler> logger,
    IMapper mapper,
    IServiceRepository serviceRepository) : IRequestHandler<CreateServicesCommand, int>
{
    public async Task<int> Handle(CreateServicesCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new Service {@Service}", request);

        var service = mapper.Map<Service>(request);

        await serviceRepository.AddAsync(service);
        return service.ServiceId;
    }
}
