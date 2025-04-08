
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.Repositories;

public class FollowUpScheduleRepository : IFollowUpScheduleRepository
{
    private readonly ApplicationDbContext _context;

    public FollowUpScheduleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(FollowUpSchedule schedule, CancellationToken cancellationToken)
    {
        await _context.FollowUpSchedules.AddAsync(schedule, cancellationToken);
    }

    public async Task<List<FollowUpSchedule>> GetPendingByContractIdAsync(int contractId, CancellationToken cancellationToken)
    {
        return await _context.FollowUpSchedules
            .Where(x => x.ContractId == contractId && x.Status == FollowUpStatus.Pending)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<FollowUpSchedule?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.FollowUpSchedules.FindAsync(new object[] { id }, cancellationToken);
    }

}
