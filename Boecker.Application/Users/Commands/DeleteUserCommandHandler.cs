
using Boecker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boecker.Application.Users.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand,Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            throw new Exception("User not found.");

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Failed to delete user: {errors}");
        }

        return Unit.Value;
    }
}

