using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Boecker.Infrastructure.Repositories
{
    public class ClientRepository(ApplicationDbContext context) : IClientRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<int> AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return client.ClientId;
        }

        public async Task DeleteAsync(Client client)
        {
            
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int clientId)
        {
            return await _context.Clients.FindAsync(clientId);
        }

        public async Task<IEnumerable<Client>> SearchAsync(string searchText)
        {
            return await _context.Clients
                .Where(c =>
                    c.Name.ToLower().Contains(searchText) ||
                    c.Email.ToLower().Contains(searchText) ||
                    c.PhoneNumber.ToLower().Contains(searchText)
                )
                .ToListAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}
