using QuotationServiceManagement.Domain.Share.Enums;

namespace QuotationServiceManagement.Application.Service.Quotation.DTOs;

public class QuotationDto
{
    public int Id { get; private set; }

    public string InquiryPartyId { get; private set; }

    public DateTime QuotationDateTime { get; private set; }

    public DateTime CreateTime { get; private set; }

    public DateTime EndTime { get; private set; }

    public QuotationStatus QuotationStatus { get; set; }

    public int QuotationCount { get; set; }

    public string Description { get; set; }

    public int TotalData { get; set; }

    public double UnitPrice { get; set; }

    public double TotalPrice { get; set; }
}