
using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface IServiceScheduleRepository
{
    Task AddAsync(ServiceSchedule schedule, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<ServiceSchedule> schedules, CancellationToken cancellationToken);
    Task<IEnumerable<ServiceSchedule>> GetByContractIdAsync(int contractId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<ServiceSchedule?> GetByIdAsync(int id, CancellationToken cancellationToken);
    IQueryable<ServiceSchedule> Query();
    Task<List<ServiceSchedule>> GetCompletedByContractIdAsync(int contractId, CancellationToken cancellationToken);
    
    Task UpdateAsync(ServiceSchedule schedule, CancellationToken cancellationToken);
    Task<List<ServiceSchedule>> GetScheduledByDateAsync(DateOnly date, CancellationToken cancellationToken);

    
    Task<List<ServiceSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task DeleteAsync(ServiceSchedule schedule, CancellationToken cancellationToken);

    

}
