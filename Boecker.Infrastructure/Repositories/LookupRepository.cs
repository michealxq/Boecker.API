
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.Repositories;

public class LookupRepository : ILookupRepository
{
    private readonly ApplicationDbContext _context;

    public LookupRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Lookup?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Lookups
            .Include(l => l.Items)
            .FirstOrDefaultAsync(l => l.Name == name, cancellationToken);
    }
}
