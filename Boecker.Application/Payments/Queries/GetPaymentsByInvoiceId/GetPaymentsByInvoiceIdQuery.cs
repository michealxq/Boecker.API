
using Boecker.Application.Payments.Dtos;
using MediatR;

namespace Boecker.Application.Payments.Queries.GetPaymentsByInvoiceId;

public class GetPaymentsByInvoiceIdQuery(int invoiceId) : IRequest<List<PaymentDto>>
{
    public int InvoiceId { get; } = invoiceId;
}
