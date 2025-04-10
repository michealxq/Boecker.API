
using Boecker.Application.Payments.Dtos;
using MediatR;

namespace Boecker.Application.Payments.Commands.CreatePaymentByInvoiceNumber;

public class CreatePaymentByInvoiceNumberCommand : IRequest<PaymentDto>
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
}
