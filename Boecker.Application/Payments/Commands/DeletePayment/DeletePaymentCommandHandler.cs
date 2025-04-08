
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Payments.Commands.DeletePayment;

public class DeletePaymentCommandHandler(IPaymentRepository paymentRepo) : IRequestHandler<DeletePaymentCommand,Unit>
{
    public async Task<Unit> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepo.GetByIdAsync(request.PaymentId);
        if (payment == null)
            throw new Exception("Payment not found.");

        await paymentRepo.DeleteAsync(payment);
        return Unit.Value;
    }
}
