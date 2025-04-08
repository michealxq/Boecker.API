using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.Repositories;

public class ServiceCategoryRepository(ApplicationDbContext context) : IServiceCategoryRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task AddAsync(ServiceCategory category)
    {
        await _context.ServiceCategories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ServiceCategory category)
    {
        context.ServiceCategories.Remove(category);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ServiceCategory>> GetAllAsync()
    {
        return await _context.ServiceCategories
        .Include(c => c.Services) 
        .ToListAsync();
    }
        


    public async Task<ServiceCategory?> GetByIdAsync(int id) =>

        await _context.ServiceCategories.FirstOrDefaultAsync(i => i.ServiceCategoryId == id);

    public async Task UpdateAsync(ServiceCategory category)
    {
        _context.ServiceCategories.Update(category);
        await _context.SaveChangesAsync();
    }
}
