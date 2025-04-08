
using AutoMapper;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.ServiceCategories.Commands.UpdateServiceCategory;

public class UpdateServiceCategoryCommandHandler : IRequestHandler<UpdateServiceCategoryCommand, Unit>
{
    private readonly IServiceCategoryRepository _repository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;

    public UpdateServiceCategoryCommandHandler(IServiceCategoryRepository repository, IServiceRepository serviceRepository, IMapper mapper)
    {
        _repository = repository;
        _serviceRepository = serviceRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateServiceCategoryCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Category.ServiceCategoryId);
        if (existing is null)
            throw new Exception("Service category not found.");

        existing.Name = request.Category.Name;
        existing.Description = request.Category.Description;

        if (request.ServiceIds is not null)
        {
            existing.Services = await _serviceRepository.GetByIdsAsync(request.ServiceIds);
        }

        await _repository.UpdateAsync(existing);
        return Unit.Value;
    }
}
