
using MediatR;

namespace Boecker.Application.Contracts.Commands.DeclineContract;

public record DeclineContractCommand(int ContractId) : IRequest<bool>;
