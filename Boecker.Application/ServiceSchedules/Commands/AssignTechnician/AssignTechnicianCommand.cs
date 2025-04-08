
using MediatR;

namespace Boecker.Application.ServiceSchedules.Commands.AssignTechnician;

public record AssignTechnicianCommand(int ScheduleId, int TechnicianId) : IRequest<bool>;
