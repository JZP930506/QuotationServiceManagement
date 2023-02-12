using MediatR;

namespace QuotationServiceManagement.Application.Service.Contract.Commands
{
    public class CreateContractCommand : IRequest<int>
    {
        public int QuotationId { get; set; }

        public int InquiryPartyId { get; set; }

        public DateTime SubmitTime { get; set; }

        public CreateContractCommand(int quotationId, int inquiryPartyId, DateTime submitTime)
        {
            QuotationId = quotationId;
            InquiryPartyId = inquiryPartyId;
            SubmitTime = submitTime;
        }
    }
}