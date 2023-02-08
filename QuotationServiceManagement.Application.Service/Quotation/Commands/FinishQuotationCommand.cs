using MediatR;

namespace QuotationServiceManagement.Application.Service.Quotation.Commands
{
    public class FinishQuotationCommand : IRequest<bool>
    {
        public int QuotationId { get; set; }

        public FinishQuotationCommand(int quotationId)
        {
            QuotationId = quotationId;
        }
    }
}