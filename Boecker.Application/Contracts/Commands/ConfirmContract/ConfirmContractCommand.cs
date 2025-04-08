
using MediatR;

namespace Boecker.Application.Contracts.Commands.ConfirmContract;

public record ConfirmContractCommand(int ContractId) : IRequest<bool>;
