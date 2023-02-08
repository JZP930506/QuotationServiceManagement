using QuotationServiceManagement.Domain.Interface;

namespace QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate
{
    public interface IInquiryPartyRepository : IRepository<InquiryParty>
    {
        InquiryParty Add(InquiryParty inquiryParty, CancellationToken cancellationToken = default);

        Task<InquiryParty> GetAsync(string title, CancellationToken cancellationToken = default);

        Task<InquiryParty> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}