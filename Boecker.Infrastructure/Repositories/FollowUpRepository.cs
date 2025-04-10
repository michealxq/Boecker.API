
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.Repositories;

public class FollowUpRepository : IFollowUpRepository
{
    private readonly ApplicationDbContext _context;

    public FollowUpRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FollowUpSchedule?> GetByIdAsync(int followUpScheduleId, CancellationToken cancellationToken = default)
    {
        return await _context.FollowUpSchedules
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.FollowUpScheduleId == followUpScheduleId, cancellationToken);
    }


    public async Task<List<FollowUpSchedule>> GetByContractIdAsync(int contractId, CancellationToken cancellationToken = default)
    {
        return await _context.FollowUpSchedules
            .Where(f => f.ContractId == contractId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FollowUpSchedule entity, CancellationToken cancellationToken = default)
    {
        await _context.FollowUpSchedules.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FollowUpSchedule entity, CancellationToken cancellationToken = default)
    {
        _context.FollowUpSchedules.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<FollowUpSchedule> followUps, CancellationToken cancellationToken)
    {
        await _context.FollowUpSchedules.AddRangeAsync(followUps, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<FollowUpSchedule> Query()
    {
        return _context.FollowUpSchedules.AsQueryable();
    }

    public async Task<List<FollowUpSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.FollowUpSchedules.ToListAsync(cancellationToken);
    }
}