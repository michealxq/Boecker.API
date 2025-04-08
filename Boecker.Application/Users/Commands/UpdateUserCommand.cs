
using MediatR;

namespace Boecker.Application.Users.Commands;

public class UpdateUserCommand : IRequest
{
    public string UserId { get; set; } = default!;
    public string Email { get; set; } = default!;
    public List<string> Roles { get; set; } = new();
}
