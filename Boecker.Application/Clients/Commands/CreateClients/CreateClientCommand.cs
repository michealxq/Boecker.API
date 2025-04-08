using MediatR;

namespace Boecker.Application.Clients.Commands.CreateClients;

public class CreateClientCommand :IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!; 
}
