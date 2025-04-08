using Boecker.Domain.Constants;

namespace Boecker.Application.Services.Dtos;

public class ServiceDto
{
    public int ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int EstimatedCompletionTime { get; set; } 
    public bool RequiresFollowUp { get; set; }
    public FollowUpPeriod? FollowUpPeriod { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int ServiceCategoryId { get; set; }
}
