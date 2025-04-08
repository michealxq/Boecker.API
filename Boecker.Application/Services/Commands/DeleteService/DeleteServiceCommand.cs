
using MediatR;

namespace Boecker.Application.Services.Commands.DeleteService;

public class DeleteServiceCommand : IRequest<Unit>
{
    public int Id { get; set; }
}