using Microsoft.EntityFrameworkCore;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.QuotationAggregate;
using QuotationServiceManagement.Domain.Interface;
using QuotationServiceManagement.Domain.Share.Enums;

namespace QuotationServiceManagement.Infrastructure.Repositories
{
    public class QuotationRepository : IQuotationRepository
    {
        private readonly QuotationServiceManagementContext _context;

        public QuotationRepository(QuotationServiceManagementContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Quotation Add(Quotation quotation, CancellationToken cancellationToken = default)
        {
            var entry = _context.Quotations.Add(quotation);
            return entry.Entity;
        }

        public Quotation Update(Quotation quotation, CancellationToken cancellationToken = default)
        {
            var entry = _context.Quotations.Update(quotation);
            return entry.Entity;
        }

        public async Task<Quotation> GetAsync(int quotationId, CancellationToken cancellationToken = default)
        {
            return await _context.Quotations.Include(t => t.QuotationItems).FirstOrDefaultAsync(t => t.Id == quotationId, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Quotation>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Quotations.Include(t => t.QuotationItems).ToListAsync(cancellationToken);
        }
    }
}