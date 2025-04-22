

using Boecker.Domain.Constants;

namespace Boecker.Application.ServiceCategories.Dtos;

public class CreateServiceDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int EstimatedCompletionTime { get; set; }
    public bool RequiresFollowUp { get; set; }
    public FollowUpPeriod? FollowUpPeriod { get; set; }
}
