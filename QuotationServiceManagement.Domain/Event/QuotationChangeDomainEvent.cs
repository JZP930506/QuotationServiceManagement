using MediatR;

namespace QuotationServiceManagement.Domain.Event
{
    public class QuotationChangeDomainEvent : INotification
    {
        public int QuotationId { get; }
        public int InquiryPartyId { get; }

        public QuotationChangeDomainEvent(
            int quotationId,
            int inquiryPartyId)
        {
            QuotationId = quotationId;
            InquiryPartyId = inquiryPartyId;
        }
    }
}