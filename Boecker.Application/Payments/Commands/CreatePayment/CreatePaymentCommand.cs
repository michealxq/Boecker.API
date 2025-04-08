
using MediatR;
using System;

namespace Boecker.Application.Payments.Commands.CreatePayment;

public record CreatePaymentCommand(
int InvoiceId,
DateTime PaymentDate,
decimal Amount,
string PaymentMethod,
string? PerformedBy = null
) : IRequest<int>;
