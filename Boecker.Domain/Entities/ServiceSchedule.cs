
using Boecker.Domain.Constants;

namespace Boecker.Domain.Entities;

public class ServiceSchedule
{
    public int ServiceScheduleId { get; set; }

    public int ContractId { get; set; }
    public Contract Contract { get; set; } = default!;

    public int? TechnicianId { get; set; }
    public Technician Technician { get; set; } = default!;

    public int ServiceId { get; set; }
    public Service Service { get; set; } = default!;

    public DateTime DateScheduled { get; set; }
    public ScheduleStatus Status { get; set; } = ScheduleStatus.Scheduled;

    public bool IsFollowUp { get; set; }
    public DateTime? DateCompleted { get; set; }
}

