

using Boecker.Application.Contracts.Dtos;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Application.Contracts.Queries.GetContractById;

public class GetContractByIdQueryHandler(IContractRepository repo)
    : IRequestHandler<GetContractByIdQuery, ContractDto?>
{
    public async Task<ContractDto?> Handle(GetContractByIdQuery request, CancellationToken cancellationToken)
    {
        var contract = await repo.Query()
            .Include(c => c.Customer)
            .Include(c => c.ServiceSchedules)
            .Include(c => c.Invoices)
            .FirstOrDefaultAsync(c => c.ContractId == request.ContractId, cancellationToken);

        return contract is null ? null : contract.ToDto();
    }
}
