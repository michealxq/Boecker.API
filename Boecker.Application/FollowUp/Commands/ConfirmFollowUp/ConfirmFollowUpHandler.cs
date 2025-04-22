
using Boecker.Application.ServiceSchedules.Commands.AssignTechnician;
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.Events;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.FollowUp.Commands.ConfirmFollowUp;

public class ConfirmFollowUpHandler(
    IMediator _mediator,
    IFollowUpRepository followUpRepo,

    IInvoiceRepository invoiceRepo,
    IServiceScheduleRepository scheduleRepo,
    ITechnicianRepository technicianRepository,
    ILogger<ConfirmFollowUpHandler> logger)
    
    : IRequestHandler<ConfirmFollowUpCommand, Unit>
{
    public async Task<Unit> Handle(ConfirmFollowUpCommand request, CancellationToken cancellationToken)
    {
        var followUp = await followUpRepo.GetByIdAsync(request.FollowUpScheduleId, cancellationToken);

        if (followUp == null) throw new Exception("Follow-up schedule not found.");

        if (followUp.Status != FollowUpStatus.Pending)
            throw new Exception("Follow-up already processed.");

        followUp.Status = FollowUpStatus.Confirmed;
        await followUpRepo.UpdateAsync(followUp, cancellationToken);

        // ✅ Create ServiceSchedule
        var invoice = await invoiceRepo.GetLatestProformaByContractIdAsync(followUp.ContractId, cancellationToken);
        if (invoice == null || invoice.InvoiceServices.Count == 0)
            throw new Exception("Cannot determine service to schedule for follow-up.");

        foreach (var invService in invoice.InvoiceServices)
        {
            var newSchedule = new ServiceSchedule
            {
                ContractId = followUp.ContractId,
                DateScheduled = followUp.ScheduledDate,
                ServiceId = invService.ServiceId,
                IsFollowUp = true,
                Status = ScheduleStatus.Scheduled,
                //TechnicianId = null
            };

            // **auto‑assign a technician**
            var tech = await technicianRepository.Query()
                    .Where(t => t.IsAvailable)
                    .OrderBy(t => t.AssignedSchedules.Count)
                    .FirstOrDefaultAsync(cancellationToken);

            if (tech != null)
            {
                newSchedule.TechnicianId = tech.TechnicianId;
                tech.IsAvailable = false;
                await technicianRepository.SaveChangesAsync(cancellationToken);

                await _mediator.Send(new AssignTechnicianCommand(newSchedule.ServiceScheduleId, tech.TechnicianId), cancellationToken);

            }

            await scheduleRepo.AddAsync(newSchedule, cancellationToken);

            logger.LogInformation("Scheduled follow-up service for contract #{ContractId} on {Date}", followUp.ContractId, followUp.ScheduledDate);
        }

        


        await followUpRepo.UpdateAsync(followUp, cancellationToken);
        await scheduleRepo.SaveChangesAsync(cancellationToken);

        // ✅ Raise event
        //await _mediator.Publish(new FollowUpConfirmedEvent(
        //    followUp.ContractId,
        //    followUp.ScheduledDate,
        //    invoice.Client.Email 
        //), cancellationToken);


        return Unit.Value;
    }
}
