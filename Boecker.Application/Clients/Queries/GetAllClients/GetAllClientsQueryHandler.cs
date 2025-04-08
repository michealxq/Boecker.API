using AutoMapper;
using Boecker.Application.Clients.Dtos;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Clients.Queries.GetAllClients;

public class GetAllClientsQueryHandler(ILogger<GetAllClientsQueryHandler> logger,
    IMapper mapper,
    IClientRepository clientRepository) : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
{
    public async Task<IEnumerable<ClientDto>> Handle (GetAllClientsQuery request , CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all Clients");
        IEnumerable<Client> client;

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            var lower = request.SearchText.ToLower();
            client = await clientRepository.SearchAsync(lower);
        }
        else
        {
            client = await clientRepository.GetAllAsync();
        }


        var clientDtos = mapper.Map<IEnumerable<ClientDto>>(client);
        return clientDtos;

    }
}
