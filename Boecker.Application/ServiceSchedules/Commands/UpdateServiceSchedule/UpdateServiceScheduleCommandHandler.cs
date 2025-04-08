
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.ServiceSchedules.Commands.UpdateServiceSchedule;

public class UpdateServiceScheduleCommandHandler(
        IServiceScheduleRepository repo
    ) : IRequestHandler<UpdateServiceScheduleCommand,Unit>
{
    public async Task<Unit> Handle(UpdateServiceScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await repo.GetByIdAsync(request.Id, cancellationToken);
        if (schedule is null) throw new Exception("Service Schedule not found");

        schedule.TechnicianId = request.TechnicianId;
        schedule.ServiceId = request.ServiceId;
        schedule.DateScheduled = request.DateScheduled;
        schedule.IsFollowUp = request.IsFollowUp;

        await repo.UpdateAsync(schedule, cancellationToken);
        return Unit.Value;
    }
}
