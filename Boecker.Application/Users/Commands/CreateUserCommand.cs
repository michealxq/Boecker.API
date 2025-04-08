
using MediatR;

namespace Boecker.Application.Users.Commands;

public class CreateUserCommand : IRequest<string>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public List<string> Roles { get; set; } = new();
}
