using MediatR;

namespace Boecker.Application.ServiceCategories.Commands.CreateServiceCategories;

public record CreateServiceCategoriesCommand(string Name, string Description, List<int> ServiceIds) : IRequest<int>;

