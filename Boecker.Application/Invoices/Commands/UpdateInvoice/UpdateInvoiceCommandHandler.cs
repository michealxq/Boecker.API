
using AutoMapper;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Invoices.Commands.UpdateInvoice;

public class UpdateInvoiceCommandHandler(
    IInvoiceRepository repo,
    IServiceRepository serviceRepo,
    IMapper mapper) : IRequestHandler<UpdateInvoiceCommand, bool>
{
    public async Task<bool> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await repo.GetByIdAsync(request.InvoiceId);
        if (invoice == null) return false;

        // Update invoice main fields
        invoice.InvoiceNumber = request.InvoiceNumber;
        invoice.TotalBeforeTax = request.TotalBeforeTax;
        invoice.VATPercentage = request.VATPercentage;
        invoice.VATAmount = request.VATAmount;
        invoice.TotalAfterTax = request.TotalAfterTax;
        invoice.IssueDate = request.IssueDate;
        invoice.ValidFrom = request.ValidFrom;
        invoice.ValidTo = request.ValidTo;
        invoice.Status = request.Status;
        invoice.IsRecurring = request.IsRecurring;
        invoice.RecurrencePeriod = request.RecurrencePeriod;
        invoice.LastGeneratedDate = request.LastGeneratedDate;

        // 💥 Replace Invoice Services
        invoice.InvoiceServices.Clear();

        foreach (var serviceDto in request.Services)
        {
            invoice.InvoiceServices.Add(new InvoiceService
            {
                ServiceId = serviceDto.ServiceId,
                Price = serviceDto.Price,
                DurationDays = serviceDto.DurationDays,
                Completed = serviceDto.Completed
            });
        }

        await repo.UpdateAsync(invoice);
        return true;
    }
}

