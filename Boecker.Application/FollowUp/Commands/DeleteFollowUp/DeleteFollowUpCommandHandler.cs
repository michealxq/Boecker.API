
using Boecker.Application.Clients.Commands.DeleteClient;
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boecker.Application.FollowUp.Commands.DeleteFollowUp;

internal class DeleteFollowUpCommandHandler(ILogger<DeleteFollowUpCommandHandler> logger,
IFollowUpRepository followUpRepository) : IRequestHandler<DeleteFollowUpCommand>
{
    public async Task Handle(DeleteFollowUpCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Follow up with id: {FollowUpScheduleId}", request.FollowUpScheduleId);
        var followUp = await followUpRepository.GetByIdAsync(request.FollowUpScheduleId);
        if (followUp is null)
            throw new NotFoundException(nameof(Client), request.FollowUpScheduleId.ToString());

        await followUpRepository.DeleteAsync(followUp);
    }
}

