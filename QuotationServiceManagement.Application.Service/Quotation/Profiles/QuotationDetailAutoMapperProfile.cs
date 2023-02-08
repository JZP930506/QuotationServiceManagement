using QuotationServiceManagement.Application.Service.Quotation.DTOs;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate;

namespace QuotationServiceManagement.Application.Service.Quotation.Profiles;

public class QuotationDetailAutoMapperProfile : AutoMapper.Profile
{
    public QuotationDetailAutoMapperProfile()
    {
        CreateMap<Domain.AggregatesModel.InquiryAggregate.QuotationAggregate.Quotation, QuotationDetailDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.CreateTime, o => o.MapFrom(s => s.CreateTime))
            .ForMember(d => d.EndTime, o => o.MapFrom(s => s.EndTime))
            .ForMember(d => d.QuotationCount, o => o.MapFrom(s => s.QuotationCount))
            .ForMember(d => d.QuotationDateTime, o => o.MapFrom(s => s.QuotationDateTime))
            .ForMember(d => d.QuotationStatus, o => o.MapFrom(s => s.QuotationStatus))
            .ForMember(d => d.TotalData, o => o.MapFrom(s => s.TotalData))
            .ForMember(d => d.UnitPrice, o => o.MapFrom(s => s.QuotationItems.Select(t => t.TotalPrice).Sum()))
            .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.QuotationItems.Select(t => t.TotalPrice).Sum() * s.TotalData));

        CreateMap<Domain.AggregatesModel.InquiryAggregate.QuotationAggregate.QuotationItem, QuotationItemsDto>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Quatity, o => o.MapFrom(s => s.Quantity))
            .ForMember(d => d.Remark, o => o.MapFrom(s => s.Remark))
            .ForMember(d => d.Specification, o => o.MapFrom(s => s.Specification))
            .ForMember(d => d.TechnologicalStandard, o => o.MapFrom(s => s.TechnologicalStandard))
            .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.UnitPrice * s.Quantity));
    }

}