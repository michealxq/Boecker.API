
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.ServiceSchedules.Commands.CreateServiceSchedule;

public class CreateServiceScheduleCommandHandler(
        IServiceScheduleRepository repo
    ) : IRequestHandler<CreateServiceScheduleCommand, int>
{
    public async Task<int> Handle(CreateServiceScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = new ServiceSchedule
        {
            ContractId = request.ContractId,
            TechnicianId = request.TechnicianId,
            ServiceId = request.ServiceId,
            DateScheduled = request.DateScheduled,
            IsFollowUp = request.IsFollowUp
        };

        await repo.AddAsync(schedule, cancellationToken);
        return schedule.ServiceScheduleId;
    }
}
