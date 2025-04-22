
using Boecker.Application.Payments.Dtos;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Payments.Queries.GetInvoicePaymentSummary;

public class GetInvoicePaymentSummaryQueryHandler
    : IRequestHandler<GetInvoicePaymentSummaryQuery, PaymentSummaryDto>
{
    private readonly IInvoiceRepository _invoices;
    private readonly IPaymentRepository _payments;

    public GetInvoicePaymentSummaryQueryHandler(
        IInvoiceRepository invoices,
        IPaymentRepository payments)
    {
        _invoices = invoices;
        _payments = payments;
    }

    public async Task<PaymentSummaryDto> Handle(
        GetInvoicePaymentSummaryQuery request,
        CancellationToken ct)
    {
        var inv = await _invoices.GetByIdAsync(request.InvoiceId);
        //if (inv is null) throw new NotFoundException(nameof(inv) ,request.InvoiceId.ToString());
        if (inv is null) return null!;

        var paid = await _payments.GetTotalPaidForInvoiceAsync(request.InvoiceId, ct);
        var remaining = inv.TotalAfterTax - paid;

        return new PaymentSummaryDto
        {
            InvoiceId = inv.InvoiceId,
            InvoiceNumber = inv.InvoiceNumber,
            TotalAfterTax = inv.TotalAfterTax,
            TotalPaid = paid,
            Remaining = remaining,
            Status = inv.Status
        };
    }
}
