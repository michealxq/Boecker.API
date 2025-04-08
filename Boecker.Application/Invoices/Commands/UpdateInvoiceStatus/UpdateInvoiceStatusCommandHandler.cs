
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Invoices.Commands.UpdateInvoiceStatus;

public class UpdateInvoiceStatusCommandHandler(ILogger<UpdateInvoiceStatusCommandHandler> logger,
    IInvoiceRepository invoiceRepository) : IRequestHandler<UpdateInvoiceStatusCommand>
{
    public async Task Handle(UpdateInvoiceStatusCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("changingInvoice status to {@Status}", request.Status);
        var invoice = await invoiceRepository.GetByIdAsync(request.InvoiceId)
                ?? throw new NotFoundException(nameof(Invoice), request.InvoiceId.ToString());

        invoice.Status = request.Status;

        var oldStatus = invoice.Status;
        invoice.Status = request.NewStatus;

        await invoiceRepository.UpdateAsync(invoice);
        await invoiceRepository.LogInvoiceStatusChangeAsync(oldStatus, request.NewStatus, invoice.InvoiceId, request.ChangedBy);
    }
}
