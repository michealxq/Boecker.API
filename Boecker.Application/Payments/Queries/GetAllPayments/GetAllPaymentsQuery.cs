

using Boecker.Application.Payments.Dtos;
using MediatR;

namespace Boecker.Application.Payments.Queries.GetAllPayments;

public class GetAllPaymentsQuery :IRequest<IEnumerable<PaymentDto>>
{
}
