
using Boecker.Domain.Constants;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Contracts.Commands.DeclineContract;

public class DeclineContractCommandHandler(
    IContractRepository contractRepo,
    IFollowUpScheduleRepository followUpScheduleRepo,
    IFollowUpRepository followUpRepo) : IRequestHandler<DeclineContractCommand, bool>
{
    public async Task<bool> Handle(DeclineContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await contractRepo.GetByIdAsync(request.ContractId, cancellationToken);
        if (contract == null) return false;

        // mark contract expired
        contract.Status = ContractStatus.Expired;
        await contractRepo.SaveChangesAsync(cancellationToken);

        // **tear down any pending follow‑ups** 
        var pending = await followUpScheduleRepo.GetPendingByContractIdAsync(request.ContractId, cancellationToken);
        foreach (var f in pending)
            await followUpRepo.DeleteAsync(f);

        return true;
    }
}
