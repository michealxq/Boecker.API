
using Boecker.Domain.Constants;
using Boecker.Domain.Entities;

namespace Boecker.Application.Reporting.Dto;

public class CustomerServiceHistoryDto
{
    public int CustomerId { get; init; }

    public string CustomerName { get; init; } = default!;
    public List<InvoiceSummaryDto> Invoices { get; init; } = new();
    public List<ServiceScheduleSummaryDto> ServiceSchedules { get; init; } = new();
}



public class InvoiceSummaryDto
{
    public int InvoiceId { get; set; }
    public string InvoiceNumber { get; set; } = default!;
    public DateTime IssueDate { get; set; }
    public decimal TotalAfterTax { get; set; }
    public string Status { get; set; } = default!;
}


public class ServiceScheduleSummaryDto
{
    public string TechnicianName { get; init; } = default!;

    public int ScheduleId { get; set; }
    public DateTime DateScheduled { get; set; }
    public string ServiceName { get; set; } = default!;
    public string Status { get; set; } = default!;
    public bool IsFollowUp { get; set; }
}


