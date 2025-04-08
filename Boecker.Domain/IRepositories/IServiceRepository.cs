using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(int id);
    Task<IEnumerable<Service>?> GetByCategoryIdAsync(int categoryId);
    Task AddAsync(Service service);
    Task DeleteAsync(Service service);
    Task<List<Service>> GetByIdsAsync(List<int> ids);
    Task UpdateAsync(Service service); 

}
