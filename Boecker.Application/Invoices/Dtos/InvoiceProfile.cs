using AutoMapper;
using Boecker.Application.Invoices.Commands.CreateInvoices;
using Boecker.Application.Invoices.Queries.GetPagedInvoices;
using Boecker.Domain.Entities;

namespace Boecker.Application.Invoices.Dtos;

public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<InvoiceService, InvoiceServiceDto>()
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Service.ServiceCategory.Name))
            .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.DurationDays, opt => opt.MapFrom(src => src.DurationDays))
            .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Completed));

        CreateMap<Client, ClientDetailsDto>();

        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.InvoiceServices))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client));



        CreateMap<CreateInvoiceCommand, Invoice>().ReverseMap()
            .ForMember(dest => dest.IsRecurring, opt => opt.MapFrom(src => src.IsRecurring))
            .ForMember(dest => dest.RecurrencePeriod, opt => opt.MapFrom(src => src.RecurrencePeriod));
        
        CreateMap<GetPagedInvoicesQuery, InvoiceDto>();
    }
}
