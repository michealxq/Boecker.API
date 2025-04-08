using AutoMapper;
using Boecker.Application.Services.Commands.CreateServices;
using Boecker.Application.Services.Queries.GetAllServices;
using Boecker.Application.Services.Queries.GetServiceById;
using Boecker.Domain.Entities;

namespace Boecker.Application.Services.Dtos;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Service, ServiceDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ServiceCategory.Name));

        CreateMap<CreateServicesCommand, Service>();
        CreateMap<GetServiceByIdQuery, Service>(); 
        CreateMap<GetAllServicesQuery, Service>();
        //CreateMap<UpdateServiceCommand, Service>();



    }
}
