
using MediatR;

namespace Boecker.Application.FollowUp.Commands.DeclineFollowUp;

public record DeclineFollowUpCommand(int FollowUpId) : IRequest<bool>;
