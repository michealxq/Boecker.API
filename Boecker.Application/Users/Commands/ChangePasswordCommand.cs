
using MediatR;

namespace Boecker.Application.Users.Commands;

public class ChangePasswordCommand : IRequest<Unit>
{
    public string CurrentPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}
