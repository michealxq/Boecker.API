
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.Contracts.Commands.DeleteContacts;

public class DeleteContactsCommandHandler(ILogger<DeleteContactsCommandHandler> logger,
IContractRepository contractRepository) : IRequestHandler<DeleteContactsCommand>
{
    public async Task Handle(DeleteContactsCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Contract with id: {ContractId}", request.ContractId);
        var contract = await contractRepository.GetByIdAsync(request.ContractId,cancellationToken);
        if (contract is null)
            throw new NotFoundException(nameof(Contract), request.ContractId.ToString());

        await contractRepository.DeleteAsync(contract);
    }
}


