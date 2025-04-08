
using MediatR;

namespace Boecker.Application.ServiceCategories.Commands.DeleteServiceCategory;

public class DeleteServiceCategoryCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
