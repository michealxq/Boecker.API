
using Boecker.Domain.Constants;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Contracts.Commands.DeclineContract;

public class DeclineContractCommandHandler(
    IContractRepository contractRepo) : IRequestHandler<DeclineContractCommand, bool>
{
    public async Task<bool> Handle(DeclineContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await contractRepo.GetByIdAsync(request.ContractId, cancellationToken);
        if (contract == null) return false;

        contract.Status = ContractStatus.Expired;
        await contractRepo.SaveChangesAsync(cancellationToken);
        return true;
    }
}
