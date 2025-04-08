
namespace Boecker.Application.Reporting.Dto;

public class TechnicianPerformanceDto
{
    public int TechnicianId { get; init; }
    public string TechnicianName { get; init; } = default!;
    public int CompletedCount { get; init; }
    public double? AverageCompletionDays { get; set; }

}



