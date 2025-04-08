using MediatR;

namespace Boecker.Application.Clients.Commands.DeleteClient;

public class DeleteClientCommand(int id) :IRequest
{
    public int ClientId { get; set; } = id;
}
