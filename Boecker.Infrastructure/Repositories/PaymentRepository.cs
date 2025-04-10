
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Payment?> GetByIdAsync(int id)
    {
        return await _context.Payments.FindAsync(id);
    }

    public async Task<IEnumerable<Payment>> GetAllByInvoiceIdAsync(int invoiceId)
    {
        return await _context.Payments
            .Include(p => p.Invoice)
            .Where(p => p.InvoiceId == invoiceId)
            .ToListAsync();
    }

    public async Task AddAsync(Payment payment, CancellationToken cancellationToken)
    {
        await _context.Payments.AddAsync(payment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Payment payment)
    {
        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Payment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Payments.ToListAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalPaidForInvoiceAsync(int invoiceId, CancellationToken cancellationToken)
    {
        return await _context.Payments
            .Where(p => p.InvoiceId == invoiceId)
            .SumAsync(p => p.Amount, cancellationToken);
    }

    public IQueryable<Payment> Query()
    {
        return _context.Payments.AsQueryable();
    }

    public async Task<List<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .Include(p => p.Invoice) // Include relationships if needed
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    => await _context.SaveChangesAsync(cancellationToken);

}
