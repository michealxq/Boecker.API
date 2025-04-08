
using Boecker.Domain.Constants;

namespace Boecker.Domain.Entities;

public class FollowUpSchedule
{
    public int FollowUpScheduleId { get; set; }

    public int ContractId { get; set; }
    public Contract Contract { get; set; } = default!;

    public DateTime ScheduledDate { get; set; }

    public FollowUpStatus Status { get; set; } = FollowUpStatus.Pending;

    public int? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
}

