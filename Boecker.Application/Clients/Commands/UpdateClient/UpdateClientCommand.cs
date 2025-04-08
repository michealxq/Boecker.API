
using MediatR;
using Boecker.Application.Clients.Dtos;

namespace Boecker.Application.Clients.Commands.UpdateClient;

public class UpdateClientCommand : IRequest<Unit>
{
    public int ClientId { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
}