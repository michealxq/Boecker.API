using Boecker.Application.ServiceCategories.Dtos;
using MediatR;

namespace Boecker.Application.ServiceCategories.Queries.GetAllServiceCategories;

public record GetAllServiceCategoriesCommand : IRequest<IEnumerable<ServiceCategoryDto>>;

