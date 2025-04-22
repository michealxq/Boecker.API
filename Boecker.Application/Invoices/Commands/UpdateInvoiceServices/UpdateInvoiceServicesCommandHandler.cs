
using AutoMapper;
using Boecker.Application.Invoices.Dtos;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Invoices.Commands.UpdateInvoiceServices;

public class UpdateInvoiceServicesCommandHandler
    : IRequestHandler<UpdateInvoiceServicesCommand, InvoiceDto>
{
    private readonly IInvoiceRepository _invoiceRepo;
    private readonly IServiceRepository _serviceRepo;
    private readonly IMapper _mapper;

    public UpdateInvoiceServicesCommandHandler(
        IInvoiceRepository invoiceRepo,
        IServiceRepository serviceRepo,
        IMapper mapper)
    {
        _invoiceRepo = invoiceRepo;
        _serviceRepo = serviceRepo;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(
        UpdateInvoiceServicesCommand request,
        CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepo.GetByIdAsync(request.InvoiceId);
        if (invoice is null) return null!; // Or throw a NotFoundException

        // 1) Replace all services
        invoice.InvoiceServices.Clear();
        foreach (var svcId in request.ServiceIds)
        {
            var svc = await _serviceRepo.GetByIdAsync(svcId);
            if (svc == null) continue;

            invoice.InvoiceServices.Add(new InvoiceService
            {
                ServiceId = svcId,
                Price = svc.Price,
                DurationDays = 1,        // or svc.EstimatedCompletionTime
                Completed = false
            });
        }

        // 2) Recalculate totals
        invoice.TotalBeforeTax = invoice.InvoiceServices.Sum(s => s.Price * s.DurationDays);
        invoice.VATAmount = invoice.TotalBeforeTax * invoice.VATPercentage / 100m;
        invoice.TotalAfterTax = invoice.TotalBeforeTax + invoice.VATAmount;

        // 3) Persist changes
        await _invoiceRepo.UpdateAsync(invoice);

        // 4) Return the mapped DTO
        return _mapper.Map<InvoiceDto>(invoice);
    }
}

