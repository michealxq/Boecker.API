
namespace Boecker.Domain.Constants.Filters;

public class InvoiceFilter
{
    public int? ClientId { get; set; }
    public bool? IsRecurring { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public InvoiceStatus? Status { get; set; }

}
