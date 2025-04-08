using Boecker.Application.Services.Dtos;
using MediatR;

namespace Boecker.Application.Services.Queries.GetServiceById;

public record GetServiceByIdQuery(int ServiceId) : IRequest<ServiceDto?>;
