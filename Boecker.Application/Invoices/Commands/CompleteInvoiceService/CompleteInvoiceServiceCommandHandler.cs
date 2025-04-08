using Boecker.Domain.Constants;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Invoices.Commands.CompleteInvoiceService;

public class CompleteInvoiceServiceCommandHandler(
    IInvoiceRepository invoiceRepository,
    IFollowUpScheduleRepository followUpRepository,
    ILogger<CompleteInvoiceServiceCommandHandler> logger)
    : IRequestHandler<CompleteInvoiceServiceCommand, Unit>
{
    public async Task<Unit> Handle(CompleteInvoiceServiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await invoiceRepository.GetInvoiceWithServicesByServiceIdAsync(request.InvoiceServiceId);
        if (invoice == null) throw new Exception("Invoice not found.");

        var service = invoice.InvoiceServices.FirstOrDefault(s => s.InvoiceServiceId == request.InvoiceServiceId);
        if (service == null) throw new Exception("Invoice service not found.");

        if (service.Completed)
            throw new Exception("Service is already marked as completed.");

        service.Completed = true;
        service.CompletionDate = DateTime.UtcNow;

        var oldStatus = invoice.Status;
        invoice.Status = request.NewStatus;

        // Auto-mark invoice as Paid if all services are completed
        if (invoice.InvoiceServices.All(s => s.Completed))
        {
            invoice.Status = InvoiceStatus.Paid;
            logger.LogInformation("All services completed — Invoice #{InvoiceId} marked as PAID.", invoice.InvoiceId);
        }

        await invoiceRepository.UpdateAsync(invoice);
        await invoiceRepository.LogInvoiceStatusChangeAsync(oldStatus, request.NewStatus, invoice.InvoiceId, request.ChangedBy);

        // 🔁 Generate FollowUpSchedule if applicable
        if (service.Service.RequiresFollowUp && service.Service.FollowUpPeriod.HasValue && invoice.ContractId.HasValue)
        {
            var followUp = new Domain.Entities.FollowUpSchedule
            {
                ContractId = invoice.ContractId.Value,
                ScheduledDate = DateTime.UtcNow.AddMonths((int)service.Service.FollowUpPeriod.Value),
                Status = FollowUpStatus.Pending,
                InvoiceId = null
            };

            await followUpRepository.AddAsync(followUp, cancellationToken);
            await followUpRepository.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Follow-up scheduled on {Date} for Contract #{ContractId}", followUp.ScheduledDate, followUp.ContractId);
        }

        return Unit.Value;
    }
}
