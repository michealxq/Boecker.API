
using Boecker.Domain.Constants;
using MediatR;

namespace Boecker.Application.Services.Commands.UpdateService;

public class UpdateServiceCommand : IRequest<Unit>
{
    public int ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int EstimatedCompletionTime { get; set; }
    public bool RequiresFollowUp { get; set; }
    public FollowUpPeriod? FollowUpPeriod { get; set; }
    public int ServiceCategoryId { get; set; }
}
