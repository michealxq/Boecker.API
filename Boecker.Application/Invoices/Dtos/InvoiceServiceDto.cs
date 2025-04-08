
namespace Boecker.Application.Invoices.Dtos;

public class InvoiceServiceDto
{
    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationDays { get; set; }
    public bool Completed { get; set; }
}

