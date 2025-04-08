namespace Boecker.Domain.Entities;

public class InvoiceService
{
    public int InvoiceServiceId { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = default!;
    public int ServiceId { get; set; }
    public Service Service { get; set; } = default!;
    public decimal Price { get; set; }
    public int DurationDays { get; set; }
    public bool Completed { get; set; }
    public DateTime? CompletionDate { get; set; }
}
