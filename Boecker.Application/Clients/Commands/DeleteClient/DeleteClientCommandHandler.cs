
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler(ILogger<DeleteClientCommandHandler> logger,
IClientRepository clientRepository) : IRequestHandler<DeleteClientCommand>
{
    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Client with id: {ClientId}", request.ClientId);
        var client = await clientRepository.GetByIdAsync(request.ClientId);
        if (client is null)
            throw new NotFoundException(nameof(Client), request.ClientId.ToString());

        await clientRepository.DeleteAsync(client);
    }

}
