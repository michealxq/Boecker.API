
using Boecker.Application.Contracts.Dtos;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Application.Contracts.Queries.GetAllContracts;

public class GetAllContractsQueryHandler(IContractRepository repo)
    : IRequestHandler<GetAllContractsQuery, List<ContractDto>>
{
    public async Task<List<ContractDto>> Handle(GetAllContractsQuery request, CancellationToken cancellationToken)
    {
        var contracts = await repo.Query()
            .Include(c => c.Customer)
            .Include(c => c.Invoices)
            .ToListAsync(cancellationToken);

        return contracts.Select(c => c.ToDto()).ToList();
    }
}
