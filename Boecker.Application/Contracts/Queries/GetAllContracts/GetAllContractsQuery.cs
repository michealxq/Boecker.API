
using Boecker.Application.Contracts.Dtos;
using MediatR;

namespace Boecker.Application.Contracts.Queries.GetAllContracts;

public record GetAllContractsQuery : IRequest<List<ContractDto>>;
