
using MediatR;

namespace Boecker.Application.Users.Commands;

public class DeleteUserCommand : IRequest<Unit>
{
    public string UserId { get; set; } = default!;
}
