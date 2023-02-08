using MediatR;
using QuotationServiceManagement.Application.Service.Quotation.DTOs;

namespace QuotationServiceManagement.Application.Service.Quotation.Commands;

public class PickQuotationsCommand : IRequest<IEnumerable<QuotationDto>>
{
    public PickQuotationsCommand()
    {
    }
}