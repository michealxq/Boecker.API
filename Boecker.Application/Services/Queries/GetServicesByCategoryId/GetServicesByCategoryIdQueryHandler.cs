
using AutoMapper;
using Boecker.Application.Services.Dtos;
using Boecker.Application.Services.Queries.GetServiceById;
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Services.Queries.GetServicesByCategoryId;

public class GetServicesByCategoryIdQueryHandler(ILogger<GetServicesByCategoryIdQueryHandler> logger,
    IServiceRepository serviceRepository,
    IMapper mapper) : IRequestHandler<GetServicesByCategoryIdQuery, IEnumerable<ServiceDto>?>
{
    public async Task<IEnumerable<ServiceDto>?> Handle(GetServicesByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Service by {CategoryId}", request.CategoryId);
        var services = await serviceRepository.GetByCategoryIdAsync(request.CategoryId)
                ?? throw new NotFoundException(nameof(Service), request.CategoryId.ToString());

        return services is null ? null : mapper.Map<IEnumerable<ServiceDto>?>(services);
    }

    
}
