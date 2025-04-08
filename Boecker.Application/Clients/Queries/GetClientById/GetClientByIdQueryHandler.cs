using AutoMapper;
using Boecker.Application.Clients.Dtos;
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Clients.Queries.GetClientById;

public class GetClientByIdQueryHandler(ILogger<GetClientByIdQueryHandler> logger,
    IClientRepository clientRepository,
    IMapper mapper) : IRequestHandler<GetClientByIdQuery, ClientDto>
{
    public async Task<ClientDto> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Client {ClientId}", request.ClientId);
        var client = await clientRepository.GetByIdAsync(request.ClientId)
                ?? throw new NotFoundException(nameof(Client), request.ClientId.ToString());

        var clientDto = mapper.Map<ClientDto>(client);

        return clientDto;
    }
}
