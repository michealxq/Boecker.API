
using MediatR;

namespace Boecker.Application.ServiceSchedules.Commands.CompleteServiceSchedule;

public record CompleteServiceScheduleCommand(int ScheduleId) : IRequest<bool>;

