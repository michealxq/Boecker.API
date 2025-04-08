
using AutoMapper;
using Boecker.Application.Clients.Commands.CreateClients;
using Boecker.Domain.Entities;

namespace Boecker.Application.Clients.Dtos;

internal class ClientProfile :Profile
{
    public ClientProfile()
    {
        CreateMap<CreateClientCommand,Client>();
        CreateMap<Client, ClientDto>();
    }
}
