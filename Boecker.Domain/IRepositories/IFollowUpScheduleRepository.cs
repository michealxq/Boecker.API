
using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface IFollowUpScheduleRepository
{
    Task AddAsync(FollowUpSchedule schedule, CancellationToken cancellationToken);
    Task<List<FollowUpSchedule>> GetPendingByContractIdAsync(int contractId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<FollowUpSchedule?> GetByIdAsync(int id, CancellationToken cancellationToken);

}
