
using MediatR;

namespace Boecker.Application.Users.Commands;

public class AssignRolesCommand :IRequest<Unit>
{
    public string UserId { get; set; } = default!;
    public List<string> Roles { get; set; } = new();
}
