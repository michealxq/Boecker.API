
using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface IInvoiceServiceRepository
{
    Task UpdateAsync(int InvoiceServiceId);
    Task<InvoiceService?> GetByIdAsync(int id);
}
