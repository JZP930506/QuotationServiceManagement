using MediatR;

namespace QuotationServiceManagement.Domain.Event
{
    public class QuotationFinishDomainEvent : INotification
    {
        public int QuotationId { get; set; }
        public int InquiryPartyId { get; set; }
        public int TotalData { get; set; }

        public string Description { get; set; }
        public DateTime SubmitTime { get; set; }

        public QuotationFinishDomainEvent(
            int quotationId,
            int inquiryPartyId,
            string description,
            int totalData,
            DateTime submitTime)
        {
            QuotationId = quotationId;
            InquiryPartyId = inquiryPartyId;
            Description = description;
            TotalData = totalData;
            SubmitTime = submitTime;
        }
    }
}