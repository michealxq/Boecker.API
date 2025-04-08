
using Boecker.Application.Invoices.Commands.CreateInvoices;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Invoices.Commands.DeleteInvoice;

public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand,Unit>
{
    private readonly IInvoiceRepository _repository;

    public DeleteInvoiceCommandHandler(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _repository.GetByIdAsync(request.InvoiceId);
        if (invoice is null)
            throw new Exception($"Invoice with ID {request.InvoiceId} not found.");

        await _repository.DeleteAsync(invoice);
        return Unit.Value;
    }
}
