using Boecker.Application.Common.Interfaces;
using Boecker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boecker.Application.Auth.Register.Command;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ITokenService _tokenService;

    public RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existing = await _userManager.FindByEmailAsync(request.Email);
        if (existing != null)
            throw new ApplicationException("User with this email already exists.");

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new ApplicationException(string.Join("; ", result.Errors.Select(e => e.Description)));

        // Ensure role exists
        if (!await _roleManager.RoleExistsAsync(request.Role))
            await _roleManager.CreateAsync(new ApplicationRole { Name = request.Role });

        await _userManager.AddToRoleAsync(user, request.Role);

        var roles = await _userManager.GetRolesAsync(user);
        return _tokenService.GenerateToken(user, roles);
    }
}

