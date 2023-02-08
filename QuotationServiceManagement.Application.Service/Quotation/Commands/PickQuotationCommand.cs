using System.Runtime.Serialization;
using MediatR;
using QuotationServiceManagement.Application.Service.Quotation.DTOs;

namespace QuotationServiceManagement.Application.Service.Quotation.Commands;

[DataContract]
public class PickQuotationCommand : IRequest<QuotationDto>
{
    [DataMember]
    public int QuotationId { get; set; }

    public PickQuotationCommand(int quotationId)
    {
        QuotationId = quotationId;
    }
}