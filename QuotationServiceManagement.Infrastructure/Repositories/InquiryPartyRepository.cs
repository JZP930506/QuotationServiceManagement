using Microsoft.EntityFrameworkCore;
using QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate;
using QuotationServiceManagement.Domain.Interface;

namespace QuotationServiceManagement.Infrastructure.Repositories
{
    public class InquiryPartyRepository : IInquiryPartyRepository
    {
        private readonly QuotationServiceManagementContext _context;

        public InquiryPartyRepository(QuotationServiceManagementContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public InquiryParty Add(InquiryParty inquiryParty, CancellationToken cancellationToken = default)
        {
            var entry = _context.InquiryParties.Add(inquiryParty);
            return entry.Entity;
        }

        public async Task<InquiryParty> GetAsync(string title, CancellationToken cancellationToken = default)
        {
            return await _context.InquiryParties.Include(t => t.BankInfo)
                .Include(t => t.LinkInfo).
                FirstOrDefaultAsync(t => t.Title == title,
                cancellationToken: cancellationToken);
        }

        public async Task<InquiryParty> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.InquiryParties.FirstOrDefaultAsync(t => t.Id == id,
                cancellationToken: cancellationToken);
        }
    }
}