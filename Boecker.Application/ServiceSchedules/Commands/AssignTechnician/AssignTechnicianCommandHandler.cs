
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.ServiceSchedules.Commands.AssignTechnician;
public class AssignTechnicianCommandHandler(
    IServiceScheduleRepository scheduleRepo,
    ITechnicianRepository technicianRepo)
    : IRequestHandler<AssignTechnicianCommand, bool>
{
    public async Task<bool> Handle(AssignTechnicianCommand request, CancellationToken cancellationToken)
    {
        var schedule = await scheduleRepo.GetByIdAsync(request.ScheduleId, cancellationToken);
        if (schedule == null) return false;

        var tech = await technicianRepo.GetByIdAsync(request.TechnicianId, cancellationToken);
        if (tech == null || !tech.IsAvailable) return false;

        schedule.TechnicianId = tech.TechnicianId;
        await scheduleRepo.SaveChangesAsync(cancellationToken);
        return true;
    }
}
