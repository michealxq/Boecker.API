using Boecker.Application.Services.Dtos;
using MediatR;

namespace Boecker.Application.Services.Queries.GetAllServices;

public record GetAllServicesQuery : IRequest<IEnumerable<ServiceDto>>;
