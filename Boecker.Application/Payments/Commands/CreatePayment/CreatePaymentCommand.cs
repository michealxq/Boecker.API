
using Boecker.Domain.Constants;
using MediatR;
using System;

namespace Boecker.Application.Payments.Commands.CreatePayment;

public record PaymentResultDto(
    int PaymentId,
    decimal TotalPaid,
    decimal Remaining,
    InvoiceStatus Status
);

public record CreatePaymentCommand(
int InvoiceId,
DateTime PaymentDate,
decimal Amount,
string PaymentMethod,
string? PerformedBy = null
) : IRequest<PaymentResultDto>;
