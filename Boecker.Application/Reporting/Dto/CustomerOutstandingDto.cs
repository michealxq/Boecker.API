
namespace Boecker.Application.Reporting.Dto;

public class CustomerOutstandingDto
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
    public decimal TotalInvoiced { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal OutstandingBalance => TotalInvoiced - TotalPaid;
}

