
using Boecker.Application.ServiceCategories.Dtos;
using MediatR;

namespace Boecker.Application.ServiceCategories.Commands.UpdateServiceCategory;

public class UpdateServiceCategoryCommand : IRequest<Unit>
{
    public ServiceCategoryDto Category { get; set; } = default!;
    public List<int> ServiceIds { get; set; } = new();
}
