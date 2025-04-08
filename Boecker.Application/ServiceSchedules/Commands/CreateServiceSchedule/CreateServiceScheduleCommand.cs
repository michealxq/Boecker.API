
using MediatR;

namespace Boecker.Application.ServiceSchedules.Commands.CreateServiceSchedule;

public record CreateServiceScheduleCommand(
    int ContractId,
    int? TechnicianId,
    int ServiceId,
    DateTime DateScheduled,
    bool IsFollowUp
) : IRequest<int>;
