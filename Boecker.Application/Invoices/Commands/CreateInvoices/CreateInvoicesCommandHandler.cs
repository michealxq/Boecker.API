using AutoMapper;
using Boecker.Application.Clients.Commands.CreateClients;
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.Events;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Invoices.Commands.CreateInvoices;

public class CreateInvoicesCommandHandler(ILogger<CreateInvoicesCommandHandler> logger,
    IInvoiceRepository invoiceRepository,
    IServiceRepository serviceRepository,
    IClientRepository clientRepository,
    IMediator mediator) : IRequestHandler<CreateInvoiceCommand, int>
{
    public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new Invoice {@Invoice}", request);


        //Validate client exists
        var client = await clientRepository.GetByIdAsync(request.ClientId);
        if (client is null)
            throw new Exception($"Client with ID {request.ClientId} not found.");

        var invoiceServices = new List<InvoiceService>();
        decimal totalBeforeTax = 0;

        foreach (var svc in request.Services)
        {
            var service = await serviceRepository.GetByIdAsync(svc.ServiceId);
            if (service is null)
                throw new Exception($"Service with ID {svc.ServiceId} not found.");

            var invoiceService = new InvoiceService
            {
                ServiceId = service.ServiceId,
                Price = service.Price,
                DurationDays = svc.DurationDays,
                Completed = false
            };

            invoiceServices.Add(invoiceService);
            totalBeforeTax += service.Price;
        }

        var vatAmount = totalBeforeTax * request.VATPercentage / 100;
        var totalAfterTax = totalBeforeTax + vatAmount;

        var invoice = new Invoice
        {
            InvoiceNumber = request.InvoiceNumber,
            IssueDate = request.IssueDate,
            DueDate = request.IssueDate.AddDays(30),
            ValidFrom = request.ValidFrom,
            ValidTo = request.ValidTo,
            VATPercentage = request.VATPercentage,
            VATAmount = vatAmount,
            TotalBeforeTax = totalBeforeTax,
            TotalAfterTax = totalAfterTax,
            Status = request.Status,
            ClientId = request.ClientId,
            InvoiceServices = invoiceServices,
            // Recurrence fields
            IsRecurring = request.IsRecurring,
            RecurrencePeriod = request.RecurrencePeriod,
            LastGeneratedDate = null // this will be set once generated
        };


        //var invoice = mapper.Map<Invoice>(request);

        await invoiceRepository.AddAsync(invoice);

        // Fetch full invoice with Client & Services
        
        var fullInvoice = await invoiceRepository.GetByIdAsync(invoice.InvoiceId);

        if (fullInvoice != null && fullInvoice.Status == InvoiceStatus.Paid)
            await mediator.Publish(new InvoiceCreatedEvent(fullInvoice), cancellationToken);

        return invoice.InvoiceId;
    }
}
