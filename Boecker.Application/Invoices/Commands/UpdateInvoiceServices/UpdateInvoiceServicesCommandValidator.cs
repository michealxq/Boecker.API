
using FluentValidation;

namespace Boecker.Application.Invoices.Commands.UpdateInvoiceServices;

public class UpdateInvoiceServicesCommandValidator
    : AbstractValidator<UpdateInvoiceServicesCommand>
{
    public UpdateInvoiceServicesCommandValidator()
    {
        RuleFor(x => x.InvoiceId).GreaterThan(0);
        RuleFor(x => x.ServiceIds).NotNull();
    }
}
