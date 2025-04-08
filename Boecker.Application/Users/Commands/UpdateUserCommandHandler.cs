
using Boecker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boecker.Application.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public UpdateUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            throw new Exception("User not found.");

        user.Email = request.Email;
        user.UserName = request.Email;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
            throw new Exception($"Failed to update user: {errors}");
        }

        // Remove all current roles
        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);

        // Add new roles
        foreach (var role in request.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new ApplicationRole { Name = role });

            await _userManager.AddToRoleAsync(user, role);
        }

        return Unit.Value;
    }

    Task IRequestHandler<UpdateUserCommand>.Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
