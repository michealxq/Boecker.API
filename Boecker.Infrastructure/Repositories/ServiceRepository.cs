using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.Repositories;

public class ServiceRepository(ApplicationDbContext context) : IServiceRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task AddAsync(Service service)
    {
        await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Service service)
    {
        context.Services.Remove(service);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Service>> GetAllAsync() =>
        await _context.Services
            .Include(s => s.ServiceCategory)
            .ToListAsync();


    public async Task<IEnumerable<Service>?> GetByCategoryIdAsync(int categoryId) =>
        await _context.Services
            .Include(s => s.ServiceCategory)
            .Where(s => s.ServiceCategoryId == categoryId)
            .ToListAsync();


    public async Task<Service?> GetByIdAsync(int id) =>
        await _context.Services
            .Include(s => s.ServiceCategory)
            .FirstOrDefaultAsync(s => s.ServiceId == id);

    public async Task<List<Service>> GetByIdsAsync(List<int> ids)
    {
        return await _context.Services
        .Where(s => ids.Contains(s.ServiceId))
        .ToListAsync();
    }

    public async Task UpdateAsync(Service service)
    {
        context.Services.Update(service);
        await context.SaveChangesAsync();
    }
}