using QuotationServiceManagement.Domain.Interface;

namespace QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.QuotationAggregate
{
    public interface IQuotationRepository : IRepository<Quotation>
    {
        Quotation Add(Quotation quotation, CancellationToken cancellationToken = default);

        Quotation Update(Quotation quotation, CancellationToken cancellationToken = default);

        Task<Quotation> GetAsync(int quotationId, CancellationToken cancellationToken = default);

        Task<IEnumerable<Quotation>> GetListAsync(CancellationToken cancellationToken = default);
    }
}