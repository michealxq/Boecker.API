
using Boecker.Application.Lookup.Dtos;
using MediatR;

namespace Boecker.Application.Lookup.Queries;

public record GetLookupByNameQuery(string Name) : IRequest<LookupDto>;
