using QuotationServiceManagement.Domain.Interface;

namespace QuotationServiceManagement.Domain.AggregatesModel.ContractAggregate;

public interface IContractRepository : IRepository<Contract>
{
    Contract Add(Contract contract, CancellationToken cancellationToken = default);
}