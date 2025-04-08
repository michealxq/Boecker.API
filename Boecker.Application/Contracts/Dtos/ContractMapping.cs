
using Boecker.Domain.Entities;

namespace Boecker.Application.Contracts.Dtos;

public static class ContractMapping
{
    public static ContractDto ToDto(this Contract contract) => new()
    {
        ContractId = contract.ContractId,
        CustomerId = contract.CustomerId,
        CustomerName = contract.Customer.Name,
        StartDate = contract.StartDate,
        EndDate = contract.EndDate,
        Status = contract.Status,
        IncludesFollowUp = contract.IncludesFollowUp
    };
}
