using Boecker.Application.Invoices.Commands.CreateInvoices;
using Boecker.Application.Invoices.Commands.GenerateProformaInvoice;
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

    public CreateContractHandler(
        IContractRepository contractRepo,
        IMediator mediator,
        IInvoiceRepository invoiceRepo,
        IServiceScheduleRepository scheduleRepo)
    {
        _contractRepo = contractRepo;
        _mediator = mediator;
        _invoiceRepo = invoiceRepo;
        _scheduleRepo = scheduleRepo;
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
                TechnicianId = null, // Or assign dynamically
                DateScheduled = contract.StartDate, // Can be calculated dynamically later
                Status = ScheduleStatus.Scheduled,
                IsFollowUp = false
            };

            await _scheduleRepo.AddAsync(schedule, cancellationToken);
        }

        await _mediator.Send(new GenerateProformaInvoiceCommand(contract.ContractId, request.VATPercentage), cancellationToken);


        return contract.ContractId;
    }
}
