
namespace Boecker.Application.Users.Dtos;

public class UserDto
{
    public string Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public List<string> Roles { get; set; } = new();
}
