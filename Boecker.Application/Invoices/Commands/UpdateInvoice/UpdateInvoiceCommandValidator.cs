
using FluentValidation;

namespace Boecker.Application.Invoices.Commands.UpdateInvoice;

public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
{
    public UpdateInvoiceCommandValidator()
    {
        RuleFor(x => x.InvoiceId).GreaterThan(0);
        RuleFor(x => x.InvoiceNumber).NotEmpty().MaximumLength(100);
        RuleFor(x => x.TotalAfterTax).GreaterThanOrEqualTo(0);
        RuleFor(x => x.VATPercentage).InclusiveBetween(0, 100);
        RuleFor(x => x.IssueDate).NotEmpty();
        RuleFor(x => x.ValidFrom).LessThanOrEqualTo(x => x.ValidTo);
    }
}