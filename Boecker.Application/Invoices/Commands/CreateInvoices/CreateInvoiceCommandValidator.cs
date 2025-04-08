using Boecker.Application.Clients.Commands.CreateClients;
using FluentValidation;

namespace Boecker.Application.Invoices.Commands.CreateInvoices;

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        

    }
}
