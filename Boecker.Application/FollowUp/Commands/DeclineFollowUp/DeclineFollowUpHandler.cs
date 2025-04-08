
using Boecker.Domain.Constants;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.FollowUp.Commands.DeclineFollowUp;

public class DeclineFollowUpHandler(IFollowUpScheduleRepository repo)
    : IRequestHandler<DeclineFollowUpCommand, bool>
{
    public async Task<bool> Handle(DeclineFollowUpCommand request, CancellationToken cancellationToken)
    {
        var followUp = await repo.GetByIdAsync(request.FollowUpId, cancellationToken);

        if (followUp == null) return false;

        followUp.Status = FollowUpStatus.Declined;
        await repo.SaveChangesAsync(cancellationToken);
        return true;
    }
}
