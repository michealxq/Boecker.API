
using Boecker.Application.Contracts.Dtos;
using MediatR;

namespace Boecker.Application.Contracts.Queries.GetContractById;

public record GetContractByIdQuery(int ContractId) : IRequest<ContractDto?>;
