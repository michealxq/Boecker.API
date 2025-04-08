
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Technicians.Commands.ToggleAvailability;

public class ToggleAvailabilityCommandHandler(ITechnicianRepository repo)
    : IRequestHandler<ToggleAvailabilityCommand, bool>
{
    public async Task<bool> Handle(ToggleAvailabilityCommand request, CancellationToken cancellationToken)
    {
        var tech = await repo.GetByIdAsync(request.TechnicianId, cancellationToken);
        if (tech == null) return false;

        tech.IsAvailable = !tech.IsAvailable;
        await repo.SaveChangesAsync(cancellationToken);
        return true;
    }
}
