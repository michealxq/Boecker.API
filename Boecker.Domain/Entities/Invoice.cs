using Boecker.Domain.Constants;

namespace Boecker.Domain.Entities;

public class Invoice
{
    public int InvoiceId { get; set; }
    public string InvoiceNumber { get; set; } = default!;
    public DateTime IssueDate { get; set; } 
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public decimal TotalBeforeTax { get; set; }
    public decimal VATPercentage { get; set; }
    public decimal VATAmount { get; set; }
    public decimal TotalAfterTax { get; set; }

    public DateTime DueDate { get; set; }

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;

    public List<Payment> Payments { get; set; } = new();

    public int? ContractId { get; set; }   // nullable to allow non-contract invoices
    public Contract? Contract { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = default!;
    public List<InvoiceService> InvoiceServices { get; set; } = new();

    //for recurring 
    public bool IsRecurring { get; set; } = false;
    public RecurrencePeriod? RecurrencePeriod { get; set; } // e.g. Yearly, Monthly
    public DateTime? LastGeneratedDate { get; set; } // tracks when last recurrence was generated
}
