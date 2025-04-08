
namespace Boecker.Application.InvoiceServices.Dtos;

public class IncompleteServiceDto
{
    public int InvoiceServiceId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
    public bool Completed { get; set; }
    public DateTime? CompletionDate { get; set; }
}