using Boecker.Application.Clients.Dtos;
using MediatR;

namespace Boecker.Application.Clients.Queries.GetClientById;

public class GetClientByIdQuery(int id) : IRequest<ClientDto>
{
    public int ClientId { get; } = id;
}
