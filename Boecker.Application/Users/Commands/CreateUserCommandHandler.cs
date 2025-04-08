
using Boecker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boecker.Application.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public CreateUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            throw new Exception("User already exists.");

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Failed to create user: {errors}");
        }

        foreach (var role in request.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new ApplicationRole { Name = role });

            await _userManager.AddToRoleAsync(user, role);
        }

        return user.Id;
    }
}

