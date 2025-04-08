
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.InvoiceServices.Commands.MarkInvoiceServiceAsCompleted;

public class MarkInvoiceServiceAsCompletedCommandHandler(ILogger<MarkInvoiceServiceAsCompletedCommandHandler> logger,
    IInvoiceServiceRepository invoiceServiceRepository)
    : IRequestHandler<MarkInvoiceServiceAsCompletedCommand>
{
    

    public async Task Handle(MarkInvoiceServiceAsCompletedCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Mark Invoice Service As Completed {@InvoiceServiceId}", request.InvoiceServiceId);
        var invoiceService = await invoiceServiceRepository.GetByIdAsync(request.InvoiceServiceId);

        if (invoiceService is null)
            throw new Exception("Invoice service not found");

        if (invoiceService.Completed)
            return; // Already completed

        await invoiceServiceRepository.UpdateAsync(request.InvoiceServiceId);
        
    }
}
