
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.Repositories;

public class ServiceScheduleRepository : IServiceScheduleRepository
{
    private readonly ApplicationDbContext _context;

    public ServiceScheduleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ServiceSchedule schedule, CancellationToken cancellationToken)
    {
        await _context.ServiceSchedules.AddAsync(schedule, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<ServiceSchedule> schedules, CancellationToken cancellationToken)
    {
        await _context.ServiceSchedules.AddRangeAsync(schedules, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ServiceSchedule>> GetByContractIdAsync(int contractId, CancellationToken cancellationToken)
    {
        return await _context.ServiceSchedules
            .Where(s => s.ContractId == contractId)
            .Include(s => s.Service)
            .Include(s => s.Technician)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken);

    public async Task<ServiceSchedule?> GetByIdAsync(int id, CancellationToken cancellationToken)
    => await _context.ServiceSchedules.FindAsync(new object[] { id }, cancellationToken);

    public IQueryable<ServiceSchedule> Query() => _context.ServiceSchedules.AsQueryable();

    public async Task<List<ServiceSchedule>> GetCompletedByContractIdAsync(int contractId, CancellationToken cancellationToken)
    {
        return await _context.ServiceSchedules
            .Include(s => s.Service)
            .Where(s => s.ContractId == contractId && s.Status == ScheduleStatus.Completed)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(ServiceSchedule schedule, CancellationToken cancellationToken)
    {
        _context.ServiceSchedules.Update(schedule);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<ServiceSchedule>> GetScheduledByDateAsync(DateOnly date, CancellationToken cancellationToken)
    {
        var from = date.ToDateTime(TimeOnly.MinValue);
        var to = date.ToDateTime(TimeOnly.MaxValue);

        return await _context.ServiceSchedules
            .Include(s => s.Contract)
                .ThenInclude(c => c.Customer)
            .Where(s =>
                s.Status == ScheduleStatus.Scheduled &&
                s.DateScheduled >= from &&
                s.DateScheduled <= to)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<ServiceSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ServiceSchedules
            .Include(s => s.Service)
            .Include(s => s.Technician)
            .Include(s => s.Contract)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(ServiceSchedule schedule, CancellationToken cancellationToken)
    {
        _context.ServiceSchedules.Remove(schedule);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
