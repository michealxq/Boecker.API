
using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface IFollowUpRepository
{
    Task<FollowUpSchedule?> GetByIdAsync(int followUpScheduleId, CancellationToken cancellationToken = default);

    Task<List<FollowUpSchedule>> GetByContractIdAsync(int contractId, CancellationToken cancellationToken = default);
    Task AddAsync(FollowUpSchedule entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(FollowUpSchedule entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<FollowUpSchedule> followUps, CancellationToken cancellationToken);
    IQueryable<FollowUpSchedule> Query();
    Task<List<FollowUpSchedule>> GetAllAsync(CancellationToken cancellationToken);

}
