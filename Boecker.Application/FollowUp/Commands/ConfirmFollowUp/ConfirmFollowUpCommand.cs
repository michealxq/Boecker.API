
using MediatR;

namespace Boecker.Application.FollowUp.Commands.ConfirmFollowUp;

public record ConfirmFollowUpCommand(int FollowUpScheduleId) : IRequest<Unit>;

