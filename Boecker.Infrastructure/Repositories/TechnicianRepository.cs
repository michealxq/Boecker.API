
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.Repositories;

public class TechnicianRepository : ITechnicianRepository
{
    private readonly ApplicationDbContext _context;

    public TechnicianRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Technician?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _context.Technicians.FindAsync(new object[] { id }, cancellationToken);

    public async Task<List<Technician>> GetAvailableAsync(CancellationToken cancellationToken)
        => await _context.Technicians
                         .Where(t => t.IsAvailable)
                         .ToListAsync(cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    => await _context.SaveChangesAsync(cancellationToken);

    public async Task AddAsync(Technician technician, CancellationToken cancellationToken)
    {
        await _context.Technicians.AddAsync(technician, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Technician technician, CancellationToken cancellationToken)
    {
        _context.Technicians.Remove(technician);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Technician>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Technicians
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public IQueryable<Technician> Query() => _context.Technicians.AsQueryable();

}
