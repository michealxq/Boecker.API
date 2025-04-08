
using Boecker.Application.Common.Interfaces;
using Boecker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boecker.Application.Auth.Login.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedAccessException("Invalid email or password");

        var roles = await _userManager.GetRolesAsync(user);
        return _tokenService.GenerateToken(user, roles);
    }
}
