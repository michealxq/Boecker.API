using Boecker.Application.Invoices.Queries.GetPagedInvoices;
using Boecker.Domain.Constants;
using Boecker.Domain.Constants.Filters;
using Boecker.Domain.Constants.Pagination;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.Extensions;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.Repositories;

public class InvoiceRepository(ApplicationDbContext context) : IInvoiceRepository
{
    public async Task AddAsync(Invoice invoice)
    {
        await context.Invoices.AddAsync(invoice);
        await context.SaveChangesAsync();
        
    }

    public async Task DeleteAsync(Invoice invoice)
    {
        context.Invoices.Remove(invoice);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync() =>
        await context.Invoices
            .Include(i => i.Client)
            .Include(i => i.InvoiceServices)
            .ThenInclude(s => s.Service)
            .ToListAsync();


    public async Task<Invoice?> GetByIdAsync(int invoiceId) =>
    
        await context.Invoices
           .Include(i => i.Client)
           .Include(i => i.InvoiceServices)
           .ThenInclude(s => s.Service)
           .ThenInclude(s => s.ServiceCategory)
           .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);

   

    public async Task<List<InvoiceService>> GetIncompleteInvoiceServicesByClientIdAsync(int clientId)
    {
        return await context.InvoiceServices
            .Where(s => !s.Completed && s.Invoice.ClientId == clientId)
            .Include(s => s.Service)
                .ThenInclude(sc => sc.ServiceCategory)
            .Include(s => s.Invoice)
                .ThenInclude(i => i.Client)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesByClientIdAsync(int clientId) =>
    
        await context.Invoices
            .Where(i => i.ClientId == clientId)
            .ToListAsync();

    public async Task<List<InvoiceStatusLog>> GetInvoiceStatusLogsAsync(int invoiceId)
    {
        return await context.InvoiceStatusLogs
            .Where(x => x.InvoiceId == invoiceId)
            .OrderByDescending(x => x.StatusChangedOn)
            .ToListAsync();
    }

    public async Task<Invoice?> GetInvoiceWithServicesByServiceIdAsync(int invoiceServiceId)
    {
        return await context.Invoices
            .Include(i => i.InvoiceServices)
            .FirstOrDefaultAsync(i => i.InvoiceServices.Any(s => s.InvoiceServiceId == invoiceServiceId));
    }

    public async Task<PaginatedResult<Invoice>> GetPagedAsync(PaginationParams pagination, InvoiceFilter? filter = null)
    {
        var query = context.Invoices
        .Include(i => i.Client)
        .Include(i => i.InvoiceServices)
            .ThenInclude(s => s.Service)
                .ThenInclude(c => c.ServiceCategory)
        .AsQueryable();

        if (filter is not null)
        {
            if (filter.ClientId.HasValue)
                query = query.Where(i => i.ClientId == filter.ClientId.Value);

            if (filter.IsRecurring.HasValue)
                query = query.Where(i => i.IsRecurring == filter.IsRecurring.Value);

            if (filter.FromDate.HasValue)
                query = query.Where(i => i.IssueDate >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(i => i.IssueDate <= filter.ToDate.Value);

            if (filter.Status.HasValue)
                query = query.Where(i => i.Status == filter.Status.Value);
        }

        // Sorting
        if (!string.IsNullOrWhiteSpace(pagination.OrderBy))
        {
            query = pagination.OrderBy.ToLower() switch
            {
                "invoiceid" => pagination.Descending ? query.OrderByDescending(i => i.InvoiceId) : query.OrderBy(i => i.InvoiceId),
                "invoicenumber" => pagination.Descending ? query.OrderByDescending(i => i.InvoiceNumber) : query.OrderBy(i => i.InvoiceNumber),
                "issuedate" => pagination.Descending ? query.OrderByDescending(i => i.IssueDate) : query.OrderBy(i => i.IssueDate),
                "totalaftertax" => pagination.Descending ? query.OrderByDescending(i => i.TotalAfterTax) : query.OrderBy(i => i.TotalAfterTax),
                _ => query.OrderByDescending(i => i.InvoiceId) // default sort now on InvoiceId
            };
        }
        else
        {
            query = query.OrderByDescending(i => i.IssueDate); // default
        }

        return await query.ToPaginatedResultAsync(pagination.PageNumber, pagination.PageSize);
    }

    public async Task<List<Invoice>> GetRecurringInvoicesAsync()
    {
        return await context.Invoices
            .Where(i => i.IsRecurring)
            .Include(i => i.InvoiceServices)
            .ToListAsync();
    }

    public async Task LinkToContractAsync(int invoiceId, int contractId, CancellationToken cancellationToken)
    {
        
        var invoice = await context.Invoices.FindAsync(new object?[] { invoiceId }, cancellationToken);
        if (invoice != null)
        {
            invoice.ContractId = contractId;
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task LogInvoiceStatusChangeAsync(InvoiceStatus oldStatus, InvoiceStatus newStatus, int invoiceId, string? changedBy = null)
    {
        var log = new InvoiceStatusLog
        {
            InvoiceId = invoiceId,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            StatusChangedOn = DateTime.UtcNow,
            ChangedBy = changedBy
        };

        await context.InvoiceStatusLogs.AddAsync(log);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Invoice invoice)
    {
        context.Invoices.Update(invoice);
        await context.SaveChangesAsync();
    }

    public async Task<Invoice?> GetLatestProformaByContractIdAsync(int contractId, CancellationToken cancellationToken)
    {
        return await context.Invoices
            .Include(i => i.InvoiceServices)
            .Where(i => i.ContractId == contractId && i.Status == InvoiceStatus.Pending)
            .OrderByDescending(i => i.IssueDate)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Invoice>> GetPendingDueByDateAsync(DateOnly dueDate, CancellationToken cancellationToken)
    {
        var from = dueDate.ToDateTime(TimeOnly.MinValue);
        var to = dueDate.ToDateTime(TimeOnly.MaxValue);

        return await context.Invoices
            .Include(i => i.Client)
            .Where(i =>
                i.Status == InvoiceStatus.Pending &&
                i.DueDate >= from &&
                i.DueDate <= to)
            .ToListAsync(cancellationToken);
    }

    public async Task<Invoice?> GetByNumberAsync(string invoiceNumber, CancellationToken cancellationToken)
    {
        // Assuming invoices have a property InvoiceNumber.
        return await context.Invoices
            .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber, cancellationToken);
    }



}
