
using Boecker.Domain.Constants;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.ServiceSchedules.Commands.CompleteServiceSchedule;

public class CompleteServiceScheduleCommandHandler(
    IServiceScheduleRepository repository) : IRequestHandler<CompleteServiceScheduleCommand, bool>
{
    public async Task<bool> Handle(CompleteServiceScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await repository.GetByIdAsync(request.ScheduleId, cancellationToken);
        if (schedule == null) return false;

        schedule.Status = ScheduleStatus.Completed;
        schedule.DateCompleted = DateTime.UtcNow;

        await repository.UpdateAsync(schedule, cancellationToken);
        return true;
    }
}

