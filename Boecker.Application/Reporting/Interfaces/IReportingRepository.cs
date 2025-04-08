using Boecker.Application.Reporting.Dto;
using Boecker.Domain.Entities;

namespace Boecker.Application.Reporting.Interfaces;

public interface IReportingRepository
{
    Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(CancellationToken cancellationToken);
    Task<List<Invoice>> GetOutstandingInvoicesAsync(CancellationToken cancellationToken);
    Task<List<MonthlyPaymentDto>> GetMonthlyPaymentsAsync(CancellationToken cancellationToken);
    Task<CustomerServiceHistoryDto> GetCustomerServiceHistoryAsync(int customerId, CancellationToken cancellationToken);
    Task<List<CustomerBalanceDto>> GetCustomerOutstandingBalancesAsync(CancellationToken cancellationToken);
    Task<List<TechnicianPerformanceDto>> GetTechnicianPerformanceAsync(DateOnly? fromDate, DateOnly? toDate, CancellationToken cancellationToken);
    Task<List<FollowUpConversionDto>> GetFollowUpConversionRatesAsync(CancellationToken cancellationToken);
    //Payment Performance: Avg days between invoice creation and full payment
    Task<double> GetAveragePaymentDurationAsync(CancellationToken cancellationToken);

    //Technician Avg Completion Time: Avg days from assignment to completion
    Task<Dictionary<string, double>> GetTechnicianAverageCompletionTimeAsync(CancellationToken cancellationToken);

}
