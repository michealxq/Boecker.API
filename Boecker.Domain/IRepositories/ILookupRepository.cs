
using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface ILookupRepository
{
    Task<Lookup?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
