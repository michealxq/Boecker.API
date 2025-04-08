
using Boecker.Domain.Entities;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Technicians.Commands.CreateTechnician;

public class CreateTechnicianCommandHandler(ITechnicianRepository repo) : IRequestHandler<CreateTechnicianCommand, int>
{
    public async Task<int> Handle(CreateTechnicianCommand request, CancellationToken cancellationToken)
    {
        var technician = new Technician
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            IsAvailable = true
        };

        await repo.AddAsync(technician, cancellationToken);
        return technician.TechnicianId;
    }
}

