using Boecker.Application.Clients.Dtos;
using MediatR;

namespace Boecker.Application.Clients.Queries.GetAllClients;

public class GetAllClientsQuery : IRequest<IEnumerable<ClientDto>>
{
    public string? SearchText { get; set; }
}
