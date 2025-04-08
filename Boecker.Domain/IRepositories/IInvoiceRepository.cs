using Boecker.Domain.Constants;
using Boecker.Domain.Constants.Filters;
using Boecker.Domain.Constants.Pagination;
using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(int invoiceId);
    Task<IEnumerable<Invoice>> GetInvoicesByClientIdAsync(int clientId);
    Task AddAsync(Invoice invoice);
    Task<IEnumerable<Invoice>> GetAllAsync();
    Task UpdateAsync(Invoice invoice);
    Task DeleteAsync(Invoice invoice);
    Task<List<Invoice>> GetRecurringInvoicesAsync();
    Task<Invoice?> GetInvoiceWithServicesByServiceIdAsync(int invoiceServiceId);
    Task<List<InvoiceService>> GetIncompleteInvoiceServicesByClientIdAsync(int clientId);
    Task LogInvoiceStatusChangeAsync(InvoiceStatus oldStatus, InvoiceStatus newStatus, int invoiceId, string? changedBy = null);

    Task<List<InvoiceStatusLog>> GetInvoiceStatusLogsAsync(int invoiceId);
    Task<PaginatedResult<Invoice>> GetPagedAsync(PaginationParams pagination, InvoiceFilter? filter );
    Task LinkToContractAsync(int invoiceId, int contractId, CancellationToken cancellationToken);
    Task<Invoice?> GetLatestProformaByContractIdAsync(int contractId, CancellationToken cancellationToken);
    Task<List<Invoice>> GetPendingDueByDateAsync(DateOnly dueDate, CancellationToken cancellationToken);



}
