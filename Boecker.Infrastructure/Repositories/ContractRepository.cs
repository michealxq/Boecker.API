
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly ApplicationDbContext _context;

    public ContractRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Contract contract, CancellationToken cancellationToken)
    {
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Contract?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Contracts
            .Include(c => c.ServiceSchedules)
                .ThenInclude(ss => ss.Service)
            .FirstOrDefaultAsync(c => c.ContractId == id, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Contract?> GetByIdWithInvoicesAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Contracts
            .Include(c => c.Invoices)
            .FirstOrDefaultAsync(c => c.ContractId == id, cancellationToken);
    }

    public IQueryable<Contract> Query() => _context.Contracts.AsQueryable();

    public async Task DeleteAsync(Contract contract)
    {
        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();
    }

    
}

