using AutoMapper;
using Boecker.Application.ServiceCategories.Commands.CreateServiceCategories;
using Boecker.Application.ServiceCategories.Queries.GetAllServiceCategories;
using Boecker.Domain.Entities;

namespace Boecker.Application.ServiceCategories.Dtos;

internal class ServiceCategoryProfile :Profile
{
    public ServiceCategoryProfile()
    {
       
        CreateMap<CreateServiceCategoriesCommand, ServiceCategory>();
        CreateMap<GetAllServiceCategoriesCommand, ServiceCategoryDto>();
       
        CreateMap<ServiceCategory, ServiceCategoryDto>()
            .ForMember(dest => dest.ServiceIds, opt => opt.MapFrom(src => src.Services.Select(s => s.ServiceId).ToList()));
    }
}
