
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boecker.Application.Payments.Dtos;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Application.Payments.Queries.GetPaymentsByInvoiceId;

public class GetPaymentsByInvoiceIdQueryHandler(
    IPaymentRepository repo,
    IMapper mapper
) : IRequestHandler<GetPaymentsByInvoiceIdQuery, List<PaymentDto>>
{
    public async Task<List<PaymentDto>> Handle(GetPaymentsByInvoiceIdQuery request, CancellationToken cancellationToken)
    {
        return await repo.Query()
                         .Where(p => p.InvoiceId == request.InvoiceId)
                         .Include(p => p.Invoice)
                         .ProjectTo<PaymentDto>(mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
    }
}
