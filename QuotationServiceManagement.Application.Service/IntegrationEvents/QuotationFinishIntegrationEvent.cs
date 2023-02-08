namespace QuotationServiceManagement.Application.Service.IntegrationEvents;

public class QuotationFinishIntegrationEvent : IntegrationEvent.IntegrationEvent
{
    public int QuotationId { get; set; }
    public int InquiryPartyId { get; set; }

    public int TotalData { get; set; }
    public DateTime SubmitTime { get; set; }

    public QuotationFinishIntegrationEvent(
        int quotationId,
        int inquiryPartyId,
        int totalData,
        DateTime submitTime)
    {
        QuotationId = quotationId;
        InquiryPartyId = inquiryPartyId;
        TotalData = totalData;
        SubmitTime = submitTime;
    }
}