using Boecker.Application.Invoices.Commands.CreateInvoices;
using Boecker.Application.Invoices.Commands.GenerateProformaInvoice;
using Boecker.Application.ServiceSchedules.Commands.AssignTechnician;
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Contracts.Commands;

public class CreateContractHandler : IRequestHandler<CreateContractCommand, int>
{
    private readonly IContractRepository _contractRepo;
    private readonly IMediator _mediator;
    private readonly IInvoiceRepository _invoiceRepo;
    private readonly IServiceScheduleRepository _scheduleRepo;
    private readonly IFollowUpRepository _followUpRepo;           // ← new
    private readonly ITechnicianRepository _technicianRepo;

    public CreateContractHandler(
        IContractRepository contractRepo,
        IMediator mediator,
        IInvoiceRepository invoiceRepo,
        IFollowUpRepository followUpRepo,                       // ← new
        ITechnicianRepository technicianRepo,

        IServiceScheduleRepository scheduleRepo)
    {
        _contractRepo = contractRepo;
        _mediator = mediator;
        _invoiceRepo = invoiceRepo;
        _scheduleRepo = scheduleRepo;
        _followUpRepo = followUpRepo;                        // ← new
        _technicianRepo = technicianRepo;
    }

    public async Task<int> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        var contract = new Contract
        {
            CustomerId = request.CustomerId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IncludesFollowUp = request.IncludesFollowUp,
            Status = ContractStatus.Active
        };

        await _contractRepo.AddAsync(contract, cancellationToken);

        var invoiceCommand = new CreateInvoiceCommand(
            InvoiceNumber: $"PROF-{DateTime.UtcNow.Ticks}",
            IssueDate: DateTime.UtcNow,
            ValidFrom: request.StartDate,
            ValidTo: request.EndDate,
            VATPercentage: request.VATPercentage,
            Status: InvoiceStatus.Pending,
            ClientId: request.CustomerId,
            Services: request.Services,
            IsRecurring: false,
            RecurrencePeriod: null
        );

        var invoiceId = await _mediator.Send(invoiceCommand, cancellationToken);

        await _invoiceRepo.LinkToContractAsync(invoiceId, contract.ContractId, cancellationToken);

        // 🛠 Auto-generate ServiceSchedules
        foreach (var service in request.Services)
        {
            var schedule = new ServiceSchedule
            {
                ContractId = contract.ContractId,
                ServiceId = service.ServiceId,
                //TechnicianId = null, 
                DateScheduled = contract.StartDate,
                Status = ScheduleStatus.Scheduled,
                IsFollowUp = false 
            };

            // pick first available technician
            var tech = (await _technicianRepo.GetAvailableAsync(cancellationToken)).FirstOrDefault();
            if (tech != null)
            {
                schedule.TechnicianId = tech.TechnicianId;
                tech.IsAvailable = false;
                await _technicianRepo.SaveChangesAsync(cancellationToken);
                
                //await _mediator.Send(new AssignTechnicianCommand(schedule.ServiceScheduleId, tech.TechnicianId), cancellationToken);
            }

            await _scheduleRepo.AddAsync(schedule, cancellationToken);
        }

        // 2) **Immediately create follow‑up** if requested  
        if (request.IncludesFollowUp)
        {
            var followUp = new FollowUpSchedule
            {
                ContractId = contract.ContractId,
                // you can derive period from your business rules; here we use 6 months:
                ScheduledDate = contract.EndDate.AddMonths(6),
                Status = FollowUpStatus.Pending
            };
            await _followUpRepo.AddAsync(followUp, cancellationToken);
        }


        await _mediator.Send(new GenerateProformaInvoiceCommand(contract.ContractId, request.VATPercentage), cancellationToken);


        return contract.ContractId;
    }
}
