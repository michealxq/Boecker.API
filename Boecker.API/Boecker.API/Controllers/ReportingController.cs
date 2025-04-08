using Boecker.Application.Reporting.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportingController(IReportingRepository reportingRepo) : ControllerBase
{
    [HttpGet("revenue/monthly")]
    public async Task<IActionResult> GetMonthlyRevenue(CancellationToken cancellationToken)
    {
        var result = await reportingRepo.GetMonthlyRevenueAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("outstanding-invoices")]
    public async Task<IActionResult> GetOutstandingInvoices(CancellationToken cancellationToken)
    {
        var result = await reportingRepo.GetOutstandingInvoicesAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("payments/monthly")]
    public async Task<IActionResult> GetMonthlyPayments(CancellationToken cancellationToken)
    {
        var result = await reportingRepo.GetMonthlyPaymentsAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("customers/{id}/history")]
    public async Task<IActionResult> GetCustomerServiceHistory(int id, CancellationToken cancellationToken)
    {
        var result = await reportingRepo.GetCustomerServiceHistoryAsync(id, cancellationToken);
        return Ok(result);
    }

    [HttpGet("customers/balances")]
    public async Task<IActionResult> GetCustomerBalances(CancellationToken cancellationToken)
    {
        var result = await reportingRepo.GetCustomerOutstandingBalancesAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("technicians/performance")]
    public async Task<IActionResult> GetTechnicianPerformance([FromQuery] DateOnly? from, [FromQuery] DateOnly? to, CancellationToken cancellationToken)
    {
        var result = await reportingRepo.GetTechnicianPerformanceAsync(from, to, cancellationToken);
        return Ok(result);
    }

    [HttpGet("followup/conversion")]
    public async Task<IActionResult> GetFollowUpConversionRates(CancellationToken cancellationToken)
    {
        var result = await reportingRepo.GetFollowUpConversionRatesAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("payments/average-duration")]
    public async Task<IActionResult> GetAveragePaymentDuration(CancellationToken cancellationToken)
    {
        var result = await reportingRepo.GetAveragePaymentDurationAsync(cancellationToken);
        return Ok(new { averageDays = result });
    }

    [HttpGet("technicians/avg-completion")]
    public async Task<IActionResult> GetTechnicianAverageCompletion(CancellationToken cancellationToken)
    {
        var result = await reportingRepo.GetTechnicianAverageCompletionTimeAsync(cancellationToken);
        return Ok(result);
    }
}
