
using Boecker.Application.Users.Dtos;
using Boecker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boecker.Application.Users.Queries;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userManager.Users.ToList();

        var result = new List<UserDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            result.Add(new UserDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Roles = roles.ToList()
            });
        }

        return result;
    }
}
