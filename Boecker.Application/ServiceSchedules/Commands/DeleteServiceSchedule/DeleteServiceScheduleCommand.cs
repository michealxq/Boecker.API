
using MediatR;

namespace Boecker.Application.ServiceSchedules.Commands.DeleteServiceSchedule;

public class DeleteServiceScheduleCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
