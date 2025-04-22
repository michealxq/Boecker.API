
using AutoMapper;
using Boecker.Domain.Entities;

namespace Boecker.Application.ServiceSchedules.Dtos;

public class ServiceScheduleProfile : Profile
{
    public ServiceScheduleProfile()
    {
        CreateMap<ServiceSchedule, ServiceScheduleDto>()
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name))
            .ForMember(dest => dest.TechnicianName, opt => opt.MapFrom(src => src.Technician.Name))
            .ForMember(dest => dest.ContractName, opt => opt.MapFrom(src => src.Contract.Customer.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name));

        CreateMap<ServiceScheduleDto, ServiceSchedule>()
            .ForMember(dest => dest.Contract, opt => opt.Ignore())
            .ForMember(dest => dest.Technician, opt => opt.Ignore())
            .ForMember(dest => dest.Service, opt => opt.Ignore());

    }
}
