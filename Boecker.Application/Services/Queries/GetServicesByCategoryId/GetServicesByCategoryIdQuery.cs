
using Boecker.Application.Services.Dtos;
using MediatR;

namespace Boecker.Application.Services.Queries.GetServicesByCategoryId;

public record GetServicesByCategoryIdQuery(int CategoryId) : IRequest<IEnumerable<ServiceDto>?>;
