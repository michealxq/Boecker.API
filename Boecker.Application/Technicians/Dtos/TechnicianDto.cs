

namespace Boecker.Application.Technicians.Dtos;

public class TechnicianDto
{
    public int TechnicianId { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public bool IsAvailable { get; set; }
}
