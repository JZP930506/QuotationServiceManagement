using QuotationServiceManagement.Domain.AggregatesModel.ContractAggregate;
using QuotationServiceManagement.Domain.Interface;

namespace QuotationServiceManagement.Infrastructure.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly QuotationServiceManagementContext _context;

        public ContractRepository(QuotationServiceManagementContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Contract Add(Contract contract, CancellationToken cancellationToken = default)
        {
            var entry = _context.Contracts.Add(contract);
            return entry.Entity;
        }

    }
}