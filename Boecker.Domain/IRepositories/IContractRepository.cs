
using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface IContractRepository
{
    Task AddAsync(Contract contract, CancellationToken cancellationToken);
    Task<Contract?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<Contract?> GetByIdWithInvoicesAsync(int id, CancellationToken cancellationToken);
    IQueryable<Contract> Query();
    Task DeleteAsync(Contract contract);
    //Task<Contract?> GetWithServicesByIdAsync(int contractId, CancellationToken cancellationToken);


}
