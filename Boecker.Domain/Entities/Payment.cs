
namespace Boecker.Domain.Entities;

public class Payment
{
    public int PaymentId { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = default!;

    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = default!; // use lookup values
}