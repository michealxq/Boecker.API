
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler(
        IPaymentRepository paymentRepo,
        IInvoiceRepository invoiceRepo
    ) : IRequestHandler<CreatePaymentCommand, int>
{
    public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var invoice = await invoiceRepo.GetByIdAsync(request.InvoiceId);
        if (invoice == null) throw new Exception("Invoice not found.");

        var payment = new Payment
        {
            InvoiceId = request.InvoiceId,
            PaymentDate = request.PaymentDate,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod
        };

        await paymentRepo.AddAsync(payment, cancellationToken);

        // Check if invoice should be marked as Paid
        var totalPaid = await paymentRepo.GetTotalPaidForInvoiceAsync(request.InvoiceId, cancellationToken);
        var shouldBeMarkedPaid = totalPaid >= invoice.TotalAfterTax;

        if (shouldBeMarkedPaid && invoice.Status != InvoiceStatus.Paid)
        {
            var oldStatus = invoice.Status;
            invoice.Status = InvoiceStatus.Paid;

            await invoiceRepo.UpdateAsync(invoice);
            await invoiceRepo.LogInvoiceStatusChangeAsync(oldStatus, InvoiceStatus.Paid, invoice.InvoiceId, request.PerformedBy);
        }

        return payment.PaymentId;
    }
}
