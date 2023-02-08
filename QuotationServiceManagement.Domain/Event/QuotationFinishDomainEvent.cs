using MediatR;

namespace QuotationServiceManagement.Domain.Event
{
    public class QuotationFinishDomainEvent : INotification
    {
        public int QuotationId { get; set; }
        public int InquiryPartyId { get; set; }
        public DateTime SubmitTime { get; set; }

        public QuotationFinishDomainEvent(
            int quotationId,
            int inquiryPartyId,
            DateTime submitTime)
        {
            QuotationId = quotationId;
            InquiryPartyId = inquiryPartyId;
            SubmitTime = submitTime;
        }
    }
}