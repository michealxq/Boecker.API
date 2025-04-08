using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(int clientId);
    Task<IEnumerable<Client>> GetAllAsync();
    Task<int> AddAsync(Client client);
    Task DeleteAsync(Client client);
    Task<IEnumerable<Client>> SearchAsync(string searchText);
    Task UpdateAsync(Client client);

}
