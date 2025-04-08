
using Boecker.Application.Reporting.Dto;
using Boecker.Application.Reporting.Interfaces;
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;
using Boecker.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.Repositories;

public class ReportingRepository : IReportingRepository
{
    private readonly ApplicationDbContext _db;

    public ReportingRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(CancellationToken cancellationToken)
    {
        return await _db.Invoices
            .Where(i => i.Status == InvoiceStatus.Paid)
            .GroupBy(i => new { i.IssueDate.Year, i.IssueDate.Month })
            .Select(g => new MonthlyRevenueDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalRevenue = g.Sum(i => i.TotalAfterTax)
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Invoice>> GetOutstandingInvoicesAsync(CancellationToken cancellationToken)
    {
        return await _db.Invoices
            .Where(i => i.Status != InvoiceStatus.Paid)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<MonthlyPaymentDto>> GetMonthlyPaymentsAsync(CancellationToken cancellationToken)
    {
        return await _db.Payments
            .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
            .Select(g => new MonthlyPaymentDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalPayments = g.Sum(p => p.Amount)
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<CustomerServiceHistoryDto> GetCustomerServiceHistoryAsync(int customerId, CancellationToken cancellationToken)
    {
        var customer = await _db.Clients
            .Include(c => c.Invoices)
            .Include(c => c.Invoices)
                .ThenInclude(i => i.InvoiceServices)
                .ThenInclude(isv => isv.Service)
            .Include(c => c.Invoices)
                .ThenInclude(i => i.Contract)
            .FirstOrDefaultAsync(c => c.ClientId == customerId, cancellationToken);

        if (customer == null)
            throw new Exception("Customer not found.");

        var schedules = await _db.ServiceSchedules
            .Where(s => s.Contract.CustomerId == customerId)
            .Select(s => new ServiceScheduleSummaryDto
            {
                DateScheduled = s.DateScheduled,
                ServiceName = s.Service.Name,
                TechnicianName = s.Technician.Name,
                Status = s.Status.ToString(),
                IsFollowUp = s.IsFollowUp
            })
            .ToListAsync(cancellationToken);

        var invoices = customer.Invoices.Select(i => new InvoiceSummaryDto
        {
            InvoiceNumber = i.InvoiceNumber,
            Status = i.Status.ToString(),
            IssueDate = i.IssueDate,
            TotalAfterTax = i.TotalAfterTax
        }).ToList();

        return new CustomerServiceHistoryDto
        {
            CustomerId = customer.ClientId,
            CustomerName = customer.Name,
            ServiceSchedules = schedules,
            Invoices = invoices
        };
    }

    public async Task<List<CustomerBalanceDto>> GetCustomerOutstandingBalancesAsync(CancellationToken cancellationToken)
    {
        var invoiceData = await _db.Invoices
            .Include(i => i.Client)
            .GroupBy(i => new { i.ClientId, i.Client.Name })
            .Select(g => new
            {
                g.Key.ClientId,
                g.Key.Name,
                TotalInvoiced = g.Sum(i => i.TotalAfterTax)
            })
            .ToListAsync(cancellationToken);

        var paymentData = await _db.Payments
            .GroupBy(p => p.Invoice.ClientId)
            .Select(g => new
            {
                ClientId = g.Key,
                TotalPaid = g.Sum(p => p.Amount)
            })
            .ToListAsync(cancellationToken);

        return invoiceData.Select(data =>
        {
            var paid = paymentData.FirstOrDefault(p => p.ClientId == data.ClientId)?.TotalPaid ?? 0;
            return new CustomerBalanceDto
            {
                ClientId = data.ClientId,
                ClientName = data.Name,
                OutstandingBalance = data.TotalInvoiced - paid
            };

        }).ToList();
    }

    public async Task<List<TechnicianPerformanceDto>> GetTechnicianPerformanceAsync(DateOnly? fromDate, DateOnly? toDate, CancellationToken cancellationToken)
    {
        var query = _db.ServiceSchedules
            .Include(s => s.Technician)
            .Where(s => s.Status == ScheduleStatus.Completed && s.TechnicianId != null && s.DateCompleted != null);

        if (fromDate.HasValue)
            query = query.Where(s => DateOnly.FromDateTime(s.DateScheduled) >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(s => DateOnly.FromDateTime(s.DateScheduled) <= toDate.Value);

        return await query
            .GroupBy(s => s.TechnicianId)
            .Select(g => new TechnicianPerformanceDto
            {
                TechnicianId = g.Key!.Value,
                TechnicianName = g.First().Technician.Name,
                CompletedCount = g.Count(),
                AverageCompletionDays = g.Average(x =>
                    EF.Functions.DateDiffDay(x.DateScheduled, x.DateCompleted!.Value))
            })
            .ToListAsync(cancellationToken);
    }


    public async Task<List<FollowUpConversionDto>> GetFollowUpConversionRatesAsync(CancellationToken cancellationToken)
    {
        var grouped = await _db.FollowUpSchedules
            .GroupBy(f => new { f.ScheduledDate.Year, f.ScheduledDate.Month })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                ConfirmedCount = g.Count(x => x.Status == FollowUpStatus.Confirmed),
                DeclinedCount = g.Count(x => x.Status == FollowUpStatus.Declined)
            })
            .ToListAsync(cancellationToken);

        return grouped.Select(x =>
        {
            var total = x.ConfirmedCount + x.DeclinedCount;
            var rate = total == 0 ? 0 : (decimal)x.ConfirmedCount / total;
            return new FollowUpConversionDto
            {
                Year = x.Year,
                Month = x.Month,
                ConfirmedCount = x.ConfirmedCount,
                DeclinedCount = x.DeclinedCount,
                ConversionRate = rate
            };
        }).ToList();
    }

    public async Task<double> GetAveragePaymentDurationAsync(CancellationToken cancellationToken)
    {
        var invoicePayments = await _db.Invoices
            .Where(i => i.Status == InvoiceStatus.Paid && i.Payments.Any())
            .Select(i => new
            {
                i.IssueDate,
                PaidDate = i.Payments
                    .OrderByDescending(p => p.PaymentDate)
                    .Select(p => p.PaymentDate)
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        if (!invoicePayments.Any())
            return 0;

        return invoicePayments
            .Select(x => (x.PaidDate - x.IssueDate).TotalDays)
            .Average();
    }

    public async Task<Dictionary<string, double>> GetTechnicianAverageCompletionTimeAsync(CancellationToken cancellationToken)
    {
        var completedSchedules = await _db.ServiceSchedules
            .Where(s => s.Status == ScheduleStatus.Completed && s.Technician != null && s.DateCompleted != null)
            .Select(s => new
            {
                s.Technician.Name,
                DaysTaken = EF.Functions.DateDiffDay(s.DateScheduled, s.DateCompleted.Value)
            })
            .ToListAsync(cancellationToken);

        return completedSchedules
            .GroupBy(s => s.Name)
            .ToDictionary(
                g => g.Key,
                g => g.Average(x => x.DaysTaken)
            );
    }


}
