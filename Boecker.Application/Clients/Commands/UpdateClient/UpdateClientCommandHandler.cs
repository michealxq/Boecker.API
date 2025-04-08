
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand,Unit>
{
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var existing = await _clientRepository.GetByIdAsync(request.ClientId);
        if (existing is null)
            throw new KeyNotFoundException($"Client with ID {request.ClientId} not found.");

        existing.Name = request.Name;
        existing.Address = request.Address;
        existing.PhoneNumber = request.PhoneNumber;
        existing.Email = request.Email;

        await _clientRepository.UpdateAsync(existing);
        return Unit.Value;
    }
}
