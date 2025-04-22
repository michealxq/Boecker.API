
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Payments.Commands.CreatePayment;



public class CreatePaymentCommandHandler(
        IPaymentRepository paymentRepo,
        IInvoiceRepository invoiceRepo
    ) : IRequestHandler<CreatePaymentCommand, PaymentResultDto>
{
    public async Task<PaymentResultDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var invoice = await invoiceRepo.GetByIdAsync(request.InvoiceId);
        if (invoice == null) return null!;
            //throw new NotFoundException("Invoice not found.");

        var payment = new Payment
        {
            InvoiceId = request.InvoiceId,
            PaymentDate = request.PaymentDate,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod
        };
        await paymentRepo.AddAsync(payment, cancellationToken);

        // recompute how much has been paid so far
        var totalPaid = await paymentRepo
            .GetTotalPaidForInvoiceAsync(request.InvoiceId, cancellationToken);

        var remaining = invoice.TotalAfterTax - totalPaid;
        var newStatus = totalPaid >= invoice.TotalAfterTax
            ? InvoiceStatus.Paid
            : InvoiceStatus.PartiallyPaid;   // make sure you add this to your enum

        if (invoice.Status != newStatus)
        {
            var old = invoice.Status;
            invoice.Status = newStatus;
            await invoiceRepo.UpdateAsync(invoice);
            await invoiceRepo.LogInvoiceStatusChangeAsync(
                old, newStatus, invoice.InvoiceId, request.PerformedBy);
        }

        return new PaymentResultDto(
            payment.PaymentId,
            totalPaid,
            Math.Max(remaining, 0m),
            newStatus
        );
    }

}
