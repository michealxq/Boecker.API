
using AutoMapper;
using Boecker.Domain.Entities;

namespace Boecker.Application.Payments.Dtos;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Payment, PaymentDto>()
            .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.Invoice.InvoiceNumber));
    }
}