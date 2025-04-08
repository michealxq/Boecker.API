
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Boecker.Infrastructure.Repositories;

public class InvoiceServiceRepository(ApplicationDbContext context) : IInvoiceServiceRepository
{
    public async Task<InvoiceService?> GetByIdAsync(int id)
    {
        var invoiceService = await context.InvoiceServices
            .FirstOrDefaultAsync(x => x.InvoiceServiceId == id);
        return invoiceService;
    }

    public async Task UpdateAsync(int InvoiceServiceId)
    {


        var invoiceService = await context.InvoiceServices
            .FirstOrDefaultAsync(x => x.InvoiceServiceId == InvoiceServiceId);

        if (invoiceService is null)
            throw new Exception("Invoice service not found");

        if (invoiceService.Completed)
            return; // Already completed

        invoiceService.Completed = true;
        

        // Check if all services in the same invoice are now completed
        bool allCompleted = await context.InvoiceServices
            .Where(s => s.InvoiceId == invoiceService.InvoiceId)
            .AllAsync(s => s.Completed);

        if (allCompleted)
        {
            invoiceService.Invoice.Status = InvoiceStatus.Paid;
        }

        await context.SaveChangesAsync();
    }
}
