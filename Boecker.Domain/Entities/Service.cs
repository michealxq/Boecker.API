using Boecker.Domain.Constants;

namespace Boecker.Domain.Entities;

public class Service
{
    public int ServiceId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int EstimatedCompletionTime { get; set; }
    public bool RequiresFollowUp { get; set; }
    public int ServiceCategoryId { get; set; }
    public FollowUpPeriod? FollowUpPeriod { get; set; }
    public ServiceCategory ServiceCategory { get; set; } = default!;
    public List<InvoiceService> InvoiceServices { get; set; } = new();

}
