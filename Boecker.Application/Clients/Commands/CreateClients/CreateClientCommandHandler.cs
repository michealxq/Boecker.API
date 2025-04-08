using AutoMapper;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Clients.Commands.CreateClients;

public class CreateClientCommandHandler(ILogger<CreateClientCommandHandler> logger,
    IMapper mapper,
    IClientRepository clientRepository) : IRequestHandler<CreateClientCommand, int>
{
    public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new Client {@Client}", request);

        var client = mapper.Map<Client>(request);

        int id = await clientRepository.AddAsync(client);
        return id;
    }
}
