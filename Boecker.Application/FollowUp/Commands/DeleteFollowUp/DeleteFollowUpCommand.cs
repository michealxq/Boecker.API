
using MediatR;

namespace Boecker.Application.FollowUp.Commands.DeleteFollowUp;

public class DeleteFollowUpCommand(int id) :IRequest
{
    public int FollowUpScheduleId { get; set; } = id;
}


