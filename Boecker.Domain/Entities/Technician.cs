
namespace Boecker.Domain.Entities;

public class Technician
{
    public int TechnicianId { get; set; }

    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;

    public bool IsAvailable { get; set; } = true;

    public ICollection<ServiceSchedule> AssignedSchedules { get; set; } = new List<ServiceSchedule>();
}
