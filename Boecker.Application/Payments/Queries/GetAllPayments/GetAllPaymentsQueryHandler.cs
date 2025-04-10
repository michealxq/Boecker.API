
using AutoMapper;
using Boecker.Application.Clients.Dtos;
using Boecker.Application.Clients.Queries.GetAllClients;
using Boecker.Application.Payments.Dtos;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Optivem.Framework.Core.Common.Http;

namespace Boecker.Application.Payments.Queries.GetAllPayments;

public class GetAllPaymentsQueryHandler(ILogger<GetAllClientsQueryHandler> logger,
    IMapper mapper,
    IPaymentRepository paymentRepository) : IRequestHandler<GetAllPaymentsQuery,IEnumerable<PaymentDto>>
{
    public async Task<IEnumerable<PaymentDto>> Handle(GetAllPaymentsQuery request , CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all Payments");
        IEnumerable<Payment> payments;

        payments = await paymentRepository.GetAllPaymentsAsync(cancellationToken);

        var paymentDto = mapper.Map<IEnumerable<PaymentDto>>(payments);
        return paymentDto;

    }
}

