
namespace Boecker.Application.ServiceSchedules.Dtos;

public class ServiceScheduleDto
{
    public int ServiceScheduleId { get; set; }
    public int ContractId { get; set; }
    public string ContractName { get; set; } = string.Empty;

    public int? TechnicianId { get; set; }
    public string? TechnicianName { get; set; }

    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;

    public DateTime DateScheduled { get; set; }
    public string Status { get; set; } = string.Empty;

    public bool IsFollowUp { get; set; }
}
