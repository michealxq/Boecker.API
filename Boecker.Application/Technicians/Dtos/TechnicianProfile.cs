
using AutoMapper;
using Boecker.Application.Technicians.Commands.CreateTechnician;
using Boecker.Domain.Entities;

namespace Boecker.Application.Technicians.Dtos;

public class TechnicianProfile : Profile
{
    public TechnicianProfile()
    {
        CreateMap<Technician, TechnicianDto>();
        CreateMap<CreateTechnicianCommand, Technician>();
    }
}
