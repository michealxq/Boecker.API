
using MediatR;

namespace Boecker.Application.ServiceSchedules.Commands.UpdateServiceSchedule;

public class UpdateServiceScheduleCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public int? TechnicianId { get; set; }
    public int ServiceId { get; set; }
    public DateTime DateScheduled { get; set; }
    public bool IsFollowUp { get; set; }
}
