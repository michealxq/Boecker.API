
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Technicians.Commands.DeleteTechnician;

public class DeleteTechnicianCommandHandler(
    ITechnicianRepository technicianRepo) : IRequestHandler<DeleteTechnicianCommand,Unit>
{
    public async Task<Unit> Handle(DeleteTechnicianCommand request, CancellationToken cancellationToken)
    {
        var technician = await technicianRepo.GetByIdAsync(request.TechnicianId, cancellationToken);
        if (technician is null) throw new Exception("Technician not found");
        await technicianRepo.DeleteAsync(technician, cancellationToken);
        return Unit.Value;
    }
}
