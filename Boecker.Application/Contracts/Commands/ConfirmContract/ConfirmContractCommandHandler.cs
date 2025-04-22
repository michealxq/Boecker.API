

using Boecker.Application.ServiceSchedules.Commands.AssignTechnician;
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Application.Contracts.Commands.ConfirmContract;

public class ConfirmContractCommandHandler(
    IMediator _mediator,
    IContractRepository contractRepo,
    IServiceScheduleRepository scheduleRepo,
    IFollowUpRepository followUpRepository,
    ITechnicianRepository techRepo,
    IInvoiceRepository invoiceRepo) : IRequestHandler<ConfirmContractCommand, bool>
{
    public async Task<bool> Handle(ConfirmContractCommand request, CancellationToken cancellationToken)
    {
        //var followUp = await followUpRepository.GetByIdAsync(req.FollowUpScheduleId, ct);
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
                IsFollowUp = true,
                //TechnicianId = null 
            };

            var tech = await techRepo.Query()
                                     .Where(t => t.IsAvailable)
                                     .OrderBy(t => t.AssignedSchedules.Count) // fewest load
                                     .FirstOrDefaultAsync(cancellationToken);
            if (tech != null)
            {
                schedule.TechnicianId = tech.TechnicianId;
                tech.IsAvailable = false;
                //await techRepo.SaveChangesAsync(cancellationToken);
                //await _mediator.Send(new AssignTechnicianCommand(schedule.ServiceScheduleId, tech.TechnicianId), cancellationToken);
            }

            await scheduleRepo.AddAsync(schedule, cancellationToken);
        }

        if (contract.IncludesFollowUp)
        {
            var followUp = new FollowUpSchedule
            {
                ContractId = contract.ContractId,
                ScheduledDate = contract.EndDate.AddMonths(6), // Example:  6 month after the contract end date
                Status = FollowUpStatus.Pending


            };

            await followUpRepository.AddAsync(followUp, cancellationToken);
        }

        

        await scheduleRepo.SaveChangesAsync(cancellationToken);
        return true;
    }
}
