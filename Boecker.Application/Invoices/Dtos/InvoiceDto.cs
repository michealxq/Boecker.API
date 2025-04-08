using Boecker.Domain.Constants;

namespace Boecker.Application.Invoices.Dtos;

public class InvoiceDto
{
    public int InvoiceId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public decimal TotalBeforeTax { get; set; } 
    public decimal VATPercentage { get; set; }
    public decimal VATAmount { get; set; }
    public decimal TotalAfterTax { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public InvoiceStatus Status { get; set; }

    public ClientDetailsDto Client { get; set; } = new();
    public List<InvoiceServiceDto> Services { get; set; } = new();

    public bool IsRecurring { get; set; }
    public RecurrencePeriod? RecurrencePeriod { get; set; }
    public DateTime? LastGeneratedDate { get; set; }
}

