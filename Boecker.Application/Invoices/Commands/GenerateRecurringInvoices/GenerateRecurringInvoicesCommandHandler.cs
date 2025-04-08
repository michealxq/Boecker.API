
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Invoices.Commands.GenerateRecurringInvoices;

public class GenerateRecurringInvoicesCommandHandler(
    IInvoiceRepository invoiceRepository,
    ILogger<GenerateRecurringInvoicesCommandHandler> logger)
    : IRequestHandler<GenerateRecurringInvoicesCommand, int>
{
    public async Task<int> Handle(GenerateRecurringInvoicesCommand request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var generatedCount = 0;

        var recurringInvoices = await invoiceRepository.GetRecurringInvoicesAsync();

        foreach (var invoice in recurringInvoices)
        {
            if (invoice.ValidTo >= now) continue;

            logger.LogInformation("Generating recurring invoice for InvoiceId: {InvoiceId}", invoice.InvoiceId);

            // Clone services
            var newServices = invoice.InvoiceServices.Select(s => new InvoiceService
            {
                ServiceId = s.ServiceId,
                Price = s.Price,
                DurationDays = s.DurationDays,
                Completed = false
            }).ToList();

            var newInvoice = new Invoice
            {
                InvoiceNumber = GenerateNewInvoiceNumber(), // You can implement your own logic
                IssueDate = now,
                ValidFrom = now,
                ValidTo = GetNextValidToDate(now, invoice.RecurrencePeriod),
                VATPercentage = invoice.VATPercentage,
                VATAmount = invoice.VATAmount,
                TotalBeforeTax = invoice.TotalBeforeTax,
                TotalAfterTax = invoice.TotalAfterTax,
                Status = InvoiceStatus.Pending,
                ClientId = invoice.ClientId,
                IsRecurring = invoice.IsRecurring,
                RecurrencePeriod = invoice.RecurrencePeriod,
                LastGeneratedDate = now,
                InvoiceServices = newServices
            };

            await invoiceRepository.AddAsync(newInvoice);

            invoice.LastGeneratedDate = now;
            await invoiceRepository.UpdateAsync(invoice);

            generatedCount++;
        }

        return generatedCount;
    }

    private DateTime GetNextValidToDate(DateTime from, RecurrencePeriod? period)
    {
        return period switch
        {
            RecurrencePeriod.Monthly => from.AddMonths(1),
            RecurrencePeriod.Quarterly => from.AddMonths(3),
            RecurrencePeriod.Yearly => from.AddYears(1),
            _ => from.AddYears(1)
        };
    }

    private string GenerateNewInvoiceNumber()
    {
        // Just an example, use your real numbering logic
        return $"INV-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
    }
}

