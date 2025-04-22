
using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface ITechnicianRepository
{
    Task<Technician?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Technician>> GetAvailableAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task AddAsync(Technician technician, CancellationToken cancellationToken);
    Task DeleteAsync(Technician technician, CancellationToken cancellationToken);
    Task<List<Technician>> GetAllAsync(CancellationToken cancellationToken);
    IQueryable<Technician> Query();

}
