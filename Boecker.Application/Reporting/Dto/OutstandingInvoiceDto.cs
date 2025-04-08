
namespace Boecker.Application.Reporting.Dto;

public class OutstandingInvoiceDto
{
    public int InvoiceId { get; set; }
    public string InvoiceNumber { get; set; } = default!;
    public DateTime DueDate { get; set; }
    public decimal TotalDue { get; set; }
    public string CustomerName { get; set; } = default!;
}

