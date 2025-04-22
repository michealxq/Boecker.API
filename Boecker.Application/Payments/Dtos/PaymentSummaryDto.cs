
using Boecker.Domain.Constants;

namespace Boecker.Application.Payments.Dtos;

public class PaymentSummaryDto
{
    public int InvoiceId { get; set; }
    public string InvoiceNumber { get; set; } = "";
    public decimal TotalAfterTax { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal Remaining { get; set; }
    public InvoiceStatus Status { get; set; }
}
