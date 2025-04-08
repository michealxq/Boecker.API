
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Invoices.Commands.GenerateProformaInvoice;

public class GenerateProformaInvoiceCommandHandler(
    IContractRepository contractRepo,
    IInvoiceRepository invoiceRepo
) : IRequestHandler<GenerateProformaInvoiceCommand, int>
{
    public async Task<int> Handle(GenerateProformaInvoiceCommand request, CancellationToken cancellationToken)
    {
        var contract = await contractRepo.GetByIdAsync(request.ContractId, cancellationToken);
        if (contract == null)
            throw new Exception("Contract not found.");

        if (contract.ServiceSchedules == null || !contract.ServiceSchedules.Any())
            throw new Exception("Contract does not have any scheduled services.");

        var invoice = new Invoice
        {
            InvoiceNumber = $"PF-{DateTime.UtcNow.Ticks}",
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

        decimal totalBeforeTax = 0;

        foreach (var schedule in contract.ServiceSchedules)
        {
            var service = schedule.Service;
            if (service == null)
                continue;

            invoice.InvoiceServices.Add(new InvoiceService
            {
                ServiceId = service.ServiceId,
                Price = service.Price,
                DurationDays = service.EstimatedCompletionTime,
                Completed = false
            });

            totalBeforeTax += service.Price;
        }

        invoice.TotalBeforeTax = totalBeforeTax;
        invoice.VATAmount = totalBeforeTax * (invoice.VATPercentage / 100);
        invoice.TotalAfterTax = invoice.TotalBeforeTax + invoice.VATAmount;

        await invoiceRepo.AddAsync(invoice);

        return invoice.InvoiceId;
    }
}
