
namespace Boecker.Application.Reporting.Dto;

public class CustomerBalanceDto
{
    public int ClientId { get; set; }
    public string ClientName { get; set; } = default!;
    public decimal OutstandingBalance { get; set; } // Not read-only anymore
}

