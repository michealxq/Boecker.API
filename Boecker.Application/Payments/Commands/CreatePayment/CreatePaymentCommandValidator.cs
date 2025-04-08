using Boecker.Application.Payments.Commands;
using FluentValidation;

namespace Boecker.Application.Payments.Commands.CreatePayment;

public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.InvoiceId)
            .GreaterThan(0).WithMessage("Invoice ID must be greater than 0.");

        RuleFor(x => x.PaymentDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Payment date cannot be in the future.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");

        RuleFor(x => x.PaymentMethod)
            .NotEmpty().WithMessage("Payment method is required.")
            .MaximumLength(50).WithMessage("Payment method is too long.");
    }
}
