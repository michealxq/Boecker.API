
namespace Boecker.Application.Invoices.Dtos;

public class InvoiceStatusLogDto
{
    public string OldStatus { get; set; } = string.Empty;
    public string NewStatus { get; set; } = string.Empty;
    public DateTime StatusChangedOn { get; set; }
    public string? ChangedBy { get; set; }
}
