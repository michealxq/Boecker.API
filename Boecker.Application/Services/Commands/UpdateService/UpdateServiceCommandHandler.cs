
using AutoMapper;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Services.Commands.UpdateService;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand,Unit>
{
    private readonly IServiceRepository _repository;
    private readonly IMapper _mapper;

    public UpdateServiceCommandHandler(IServiceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _repository.GetByIdAsync(request.ServiceId);

        if (service is null)
            throw new Exception($"Service with ID {request.ServiceId} not found.");

        // Update fields
        service.Name = request.Name;
        service.Description = request.Description;
        service.Price = request.Price;
        service.EstimatedCompletionTime = request.EstimatedCompletionTime;
        service.RequiresFollowUp = request.RequiresFollowUp;
        service.FollowUpPeriod = request.FollowUpPeriod;
        service.ServiceCategoryId = request.ServiceCategoryId;

        await _repository.UpdateAsync(service);

        return Unit.Value;
    }
}
