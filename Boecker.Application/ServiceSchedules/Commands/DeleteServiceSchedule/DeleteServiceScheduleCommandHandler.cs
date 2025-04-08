
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.ServiceSchedules.Commands.DeleteServiceSchedule;

public class DeleteServiceScheduleCommandHandler(
        IServiceScheduleRepository repo
    ) : IRequestHandler<DeleteServiceScheduleCommand,Unit>
{
    public async Task<Unit> Handle(DeleteServiceScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await repo.GetByIdAsync(request.Id, cancellationToken);
        if (schedule == null) throw new Exception("Service Schedule not found");

        await repo.DeleteAsync(schedule, cancellationToken);
        return Unit.Value;
    }
}
