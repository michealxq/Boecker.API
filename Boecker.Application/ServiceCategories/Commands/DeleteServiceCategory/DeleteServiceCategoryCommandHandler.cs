
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.ServiceCategories.Commands.DeleteServiceCategory;

public class DeleteServiceCategoryCommandHandler : IRequestHandler<DeleteServiceCategoryCommand,Unit>
{
    private readonly IServiceCategoryRepository _repository;

    public DeleteServiceCategoryCommandHandler(IServiceCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteServiceCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id);
        if (category == null)
            throw new Exception($"Service category with ID {request.Id} not found.");

        await _repository.DeleteAsync(category);
        return Unit.Value;
    }
}