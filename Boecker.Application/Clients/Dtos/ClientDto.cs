
namespace Boecker.Application.Clients.Dtos;

public class ClientDto
{
    public int ClientId { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
}
