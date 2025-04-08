using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface IServiceCategoryRepository
{
    Task<IEnumerable<ServiceCategory>> GetAllAsync();
    Task<ServiceCategory?> GetByIdAsync(int id);
    Task AddAsync(ServiceCategory category);
    Task DeleteAsync(ServiceCategory category);
    Task UpdateAsync(ServiceCategory category);
}
