

using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Contracts.Commands.ConfirmContract;

public class ConfirmContractCommandHandler(
    IContractRepository contractRepo,
    IServiceScheduleRepository scheduleRepo,
    IFollowUpRepository followUpRepository,
    IInvoiceRepository invoiceRepo) : IRequestHandler<ConfirmContractCommand, bool>
{
    public async Task<bool> Handle(ConfirmContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await contractRepo.GetByIdAsync(request.ContractId, cancellationToken);
        if (contract == null) return false;



        contract.Status = ContractStatus.Active;
        await contractRepo.SaveChangesAsync(cancellationToken);

        var invoice = await invoiceRepo.GetLatestProformaByContractIdAsync(contract.ContractId, cancellationToken);
        if (invoice == null) return false;



        foreach (var invoiceService in invoice.InvoiceServices)
        {
            var schedule = new Domain.Entities.ServiceSchedule
            {
                ContractId = contract.ContractId,
                ServiceId = invoiceService.ServiceId,
                DateScheduled = contract.StartDate,
                Status = ScheduleStatus.Scheduled,
                IsFollowUp = false,
                TechnicianId = null // can be assigned later
            };

            await scheduleRepo.AddAsync(schedule, cancellationToken);
        }

        if (contract.IncludesFollowUp)
        {
            var followUp = new FollowUpSchedule
            {
                ContractId = contract.ContractId,
                ScheduledDate = contract.EndDate.AddMonths(1), // Example: 1 month after the contract end date
                Status = FollowUpStatus.Pending


            };

            await followUpRepository.AddAsync(followUp, cancellationToken);
        }

            await scheduleRepo.SaveChangesAsync(cancellationToken);
        return true;
    }
}
