
using Boecker.Domain.Constants;

namespace Boecker.Domain.Entities;

public class InvoiceStatusLog
{
    public int InvoiceStatusLogId { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = default!;

    public InvoiceStatus OldStatus { get; set; }
    public InvoiceStatus NewStatus { get; set; }

    public DateTime StatusChangedOn { get; set; } = DateTime.UtcNow;

    public string? ChangedBy { get; set; } 
}
