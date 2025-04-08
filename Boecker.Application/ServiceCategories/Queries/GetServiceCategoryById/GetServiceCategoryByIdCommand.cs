using Boecker.Application.ServiceCategories.Dtos;
using MediatR;

namespace Boecker.Application.ServiceCategories.Queries.GetServiceCategoryById;

public record GetServiceCategoryByIdCommand(int ServiceCategoryId) : IRequest<ServiceCategoryDto?>;

