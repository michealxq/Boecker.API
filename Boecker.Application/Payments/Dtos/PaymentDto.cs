
namespace Boecker.Application.Payments.Dtos;

public class PaymentDto
{
    public int PaymentId { get; set; }
    public int InvoiceId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
}
