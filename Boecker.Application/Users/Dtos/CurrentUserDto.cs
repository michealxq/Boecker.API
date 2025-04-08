
namespace Boecker.Application.Users.Dtos;

public class CurrentUserDto
{
    public string Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public List<string> Roles { get; set; } = new();
}
