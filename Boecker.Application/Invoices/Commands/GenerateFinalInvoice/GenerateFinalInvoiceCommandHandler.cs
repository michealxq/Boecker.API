
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Invoices.Commands.GenerateFinalInvoice;

public class GenerateFinalInvoiceCommandHandler(
    IContractRepository contractRepo,
    IInvoiceRepository invoiceRepo,
    IServiceScheduleRepository scheduleRepo,
    IFollowUpRepository followUpRepo
) : IRequestHandler<GenerateFinalInvoiceCommand, int>
{
    public async Task<int> Handle(GenerateFinalInvoiceCommand request, CancellationToken cancellationToken)
    {
        var contract = await contractRepo.GetByIdAsync(request.ContractId, cancellationToken);
        if (contract == null) throw new Exception("Contract not found.");

        var completedSchedules = await scheduleRepo.GetCompletedByContractIdAsync(request.ContractId, cancellationToken);
        if (!completedSchedules.Any()) throw new Exception("No completed services to invoice.");

        var invoice = new Invoice
        {
            InvoiceNumber = $"INV-{DateTime.UtcNow.Ticks}",
            IssueDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(30),
            ValidFrom = contract.StartDate,
            ValidTo = contract.EndDate,
            ClientId = contract.CustomerId,
            ContractId = contract.ContractId,
            Status = InvoiceStatus.Pending,
            IsRecurring = false,
            VATPercentage = request.VATPercentage,
        };

        decimal total = 0;
        var followUps = new List<FollowUpSchedule>();

        foreach (var schedule in completedSchedules)
        {
            var service = schedule.Service;
            var price = service.Price;
            total += price;

            invoice.InvoiceServices.Add(new InvoiceService
            {
                ServiceId = service.ServiceId,
                Price = price,
                DurationDays = service.EstimatedCompletionTime,
                Completed = true,
                CompletionDate = schedule.DateScheduled
            });

            if (contract.IncludesFollowUp && service.RequiresFollowUp && service.FollowUpPeriod.HasValue)
            {
                var followUpDate = schedule.DateScheduled.AddMonths((int)service.FollowUpPeriod.Value);
                followUps.Add(new FollowUpSchedule
                {
                    ContractId = contract.ContractId,
                    ScheduledDate = followUpDate,
                    Status = FollowUpStatus.Pending
                });
            }
        }

        invoice.TotalBeforeTax = total;
        invoice.VATAmount = total * (request.VATPercentage / 100);
        invoice.TotalAfterTax = invoice.TotalBeforeTax + invoice.VATAmount;

        await invoiceRepo.AddAsync(invoice);

        // Save follow-up schedules
        if (followUps.Any())
            await followUpRepo.AddRangeAsync(followUps, cancellationToken);

        return invoice.InvoiceId;
    }
}
