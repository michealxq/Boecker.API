
using Boecker.Domain.Entities;

namespace Boecker.Domain.IRepositories;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(int id);
    Task<IEnumerable<Payment>> GetAllByInvoiceIdAsync(int invoiceId);
    Task AddAsync(Payment payment, CancellationToken cancellationToken);
    Task DeleteAsync(Payment payment);
    Task<List<Payment>> GetAllAsync(CancellationToken cancellationToken);
    Task<decimal> GetTotalPaidForInvoiceAsync(int invoiceId, CancellationToken cancellationToken);
    IQueryable<Payment> Query();
    Task<List<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
