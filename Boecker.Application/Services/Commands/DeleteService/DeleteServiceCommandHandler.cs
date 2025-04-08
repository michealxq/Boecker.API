
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Services.Commands.DeleteService;

public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand,Unit>
{
    private readonly IServiceRepository _repository;

    public DeleteServiceCommandHandler(IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _repository.GetByIdAsync(request.Id);
        if (service == null)
            throw new Exception($"Service with ID {request.Id} not found.");

        await _repository.DeleteAsync(service);
        return Unit.Value;
    }
}
