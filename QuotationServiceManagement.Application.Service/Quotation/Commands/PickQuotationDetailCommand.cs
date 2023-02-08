using MediatR;
using QuotationServiceManagement.Application.Service.Quotation.DTOs;

namespace QuotationServiceManagement.Application.Service.Quotation.Commands;

public class PickQuotationDetailCommand : IRequest<QuotationDetailDto>
{
    public int QuotationId { get; set; }

    public PickQuotationDetailCommand(int quotationId)
    {
        QuotationId = quotationId;
    }
}