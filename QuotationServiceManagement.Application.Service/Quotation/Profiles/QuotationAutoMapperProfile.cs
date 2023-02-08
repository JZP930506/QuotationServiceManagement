using QuotationServiceManagement.Application.Service.Quotation.DTOs;

namespace QuotationServiceManagement.Application.Service.Quotation.Profiles;

public class QuotationAutoMapperProfile : AutoMapper.Profile
{
    public QuotationAutoMapperProfile()
    {
        CreateMap<Domain.AggregatesModel.InquiryAggregate.QuotationAggregate.Quotation, QuotationDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.CreateTime, o => o.MapFrom(s => s.CreateTime))
            .ForMember(d => d.EndTime, o => o.MapFrom(s => s.EndTime))
            .ForMember(d => d.QuotationCount, o => o.MapFrom(s => s.QuotationCount))
            .ForMember(d => d.QuotationDateTime, o => o.MapFrom(s => s.QuotationDateTime))
            .ForMember(d => d.QuotationStatus, o => o.MapFrom(s => s.QuotationStatus))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.TotalData, o => o.MapFrom(s => s.TotalData))
            .ForMember(d => d.UnitPrice, o => o.MapFrom(s => s.QuotationItems.Select(t => t.TotalPrice).Sum()))
            .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.QuotationItems.Select(t => t.TotalPrice).Sum() * s.TotalData));
    }
}