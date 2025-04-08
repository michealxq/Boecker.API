
using Boecker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boecker.Application.Users.Commands;

public class AssignRolesCommandHandler : IRequestHandler<AssignRolesCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AssignRolesCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(AssignRolesCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            throw new Exception("User not found.");

        var currentRoles = await _userManager.GetRolesAsync(user);
        var rolesToRemove = currentRoles.Except(request.Roles);
        var rolesToAdd = request.Roles.Except(currentRoles);

        await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

        foreach (var role in rolesToAdd)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new ApplicationRole { Name = role });

            await _userManager.AddToRoleAsync(user, role);
        }

        return Unit.Value;
    }

    
}
