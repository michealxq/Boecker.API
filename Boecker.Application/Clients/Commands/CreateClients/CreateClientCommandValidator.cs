using FluentValidation;

namespace Boecker.Application.Clients.Commands.CreateClients;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);


        RuleFor(dto => dto.Email)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");

        

        RuleFor(dto => dto.PhoneNumber)
            .Matches(@"^+\d{3}-\d{8}$")
            .WithMessage("Please provide a valid phone number (+XXX-XXXXXXXX).");

    }

}
