
using MediatR;

namespace Boecker.Application.Payments.Commands.DeletePayment;

public record DeletePaymentCommand(int PaymentId) : IRequest<Unit>;
