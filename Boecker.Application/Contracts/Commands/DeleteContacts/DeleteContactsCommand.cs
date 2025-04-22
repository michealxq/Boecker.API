
using MediatR;

namespace Boecker.Application.Contracts.Commands.DeleteContacts;

public class DeleteContactsCommand(int id) : IRequest
{
    public int ContractId { get; set; } = id;
}


