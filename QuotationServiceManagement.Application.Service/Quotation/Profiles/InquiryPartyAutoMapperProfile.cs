using QuotationServiceManagement.Application.Service.Quotation.DTOs;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate;

namespace QuotationServiceManagement.Application.Service.Quotation.Profiles
{
    public class InquiryPartyAutoMapperProfile : AutoMapper.Profile
    {
        public InquiryPartyAutoMapperProfile()
        {
            CreateMap<InquiryParty, InquiryPartyDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.LinkMan, o => o.MapFrom(s => s.LinkInfo.LinkMan))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.LinkInfo.Email))
                .ForMember(d => d.Phone, o => o.MapFrom(s => s.LinkInfo.Phone))
                .ForMember(d => d.Fax, o => o.MapFrom(s => s.LinkInfo.Fax))
                .ForMember(d => d.BankAccount, o => o.MapFrom(s => s.BankInfo.BankAccount))
                .ForMember(d => d.OpeningBank, o => o.MapFrom(s => s.BankInfo.OpeningBank));
        }
    }
}