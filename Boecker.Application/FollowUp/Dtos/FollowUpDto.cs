
namespace Boecker.Application.FollowUp.Dtos;

public class FollowUpDto
{
    public int FollowUpScheduleId { get; set; }
    public int ContractId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? InvoiceId { get; set; }
    // Add any other fields if needed, e.g. ClientName, etc.
}
