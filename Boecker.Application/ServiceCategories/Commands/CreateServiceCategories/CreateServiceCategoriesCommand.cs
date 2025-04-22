using Boecker.Application.ServiceCategories.Dtos;
using MediatR;

namespace Boecker.Application.ServiceCategories.Commands.CreateServiceCategories;

public record CreateServiceCategoriesCommand(
    string Name,
    string Description,
    List<CreateServiceDto> Services
) : IRequest<int>;


