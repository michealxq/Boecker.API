
using MediatR;

namespace Boecker.Application.Technicians.Commands.DeleteTechnician;

public record DeleteTechnicianCommand(int TechnicianId) : IRequest<Unit>;
