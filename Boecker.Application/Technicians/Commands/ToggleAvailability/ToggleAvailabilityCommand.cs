
using MediatR;

namespace Boecker.Application.Technicians.Commands.ToggleAvailability;

public record ToggleAvailabilityCommand(int TechnicianId) : IRequest<bool>;
