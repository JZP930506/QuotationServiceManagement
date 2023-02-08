using System.Runtime.Serialization;
using MediatR;
using QuotationServiceManagement.Application.Service.Quotation.DTOs;

namespace QuotationServiceManagement.Application.Service.Quotation.Commands
{
    [DataContract]
    public class PickInquiryPartyCommand : IRequest<InquiryPartyDto>
    {
        [DataMember]
        public string Title { get; set; }

        public PickInquiryPartyCommand(string title)
        {
            Title = title;
        }
    }
}