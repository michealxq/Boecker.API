
using Boecker.Domain.Constants;

namespace Boecker.Application.Contracts.Dtos;

public class ContractDto
{
    public int ContractId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ContractStatus Status { get; set; }
    public bool IncludesFollowUp { get; set; }
}