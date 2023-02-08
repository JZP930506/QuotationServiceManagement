namespace QuotationServiceManagement.Domain.AggregatesModel.ContractAggregate;

public class ContractBuilder
{
    private readonly Contract _contract;

    public ContractBuilder()
    {
        _contract = new Contract();
    }

    public ContractBuilder InitContractTime(DateTime createTime,int quotationId, string description, int totalData)
    {
        _contract.InitContract(createTime,quotationId, description, totalData);
        return this;
    }

    public ContractBuilder SetDeliveryTime(TimeSpan timeSpan)
    {
        _contract.SetDeliveryTime(timeSpan);
        return this;
    }

    public ContractBuilder CalculateUnitPrice()
    {
        if (_contract.ContractItems.Any())
            throw new ArgumentException($"{nameof(_contract.ContractItems)} can't be null or empty.");

        var totalPrice = _contract.ContractItems.Select(t => t.TotalPrice).Sum();
        if (totalPrice < 0)
            throw new ArgumentException($"{nameof(_contract.UnitPrice)} must great than zero.");

        _contract.UnitPrice = totalPrice;
        return this;
    }

    public ContractBuilder SetFirstParty(string title, string address, LinkInfo linkInfo, BankInfo bankInfo)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException($"{nameof(title)} can't be null or empty.");

        if (string.IsNullOrEmpty(address))
            throw new ArgumentException($"{nameof(address)} can't be null or empty.");

        _contract.FirstParty.Title = title;
        _contract.FirstParty.Address = address;
        _contract.FirstParty.BankInfo = bankInfo;
        _contract.FirstParty.LinkInfo = linkInfo;
        return this;
    }
    
    public ContractBuilder AddContractItems(string name, string specification, string technologicalStandard, double unitPrice, int quatity, string remark)
    {
        _contract.AddContractItem(name, specification, technologicalStandard, unitPrice, quatity, remark);
        return this;
    }

    public Contract Build()
    {
        if (_contract.UnitPrice <= 0)
            throw new ArgumentException($"{nameof(_contract.UnitPrice)} must great than zero.");

        if (_contract.ContractItems.Any())
            throw new ArgumentException($"{nameof(_contract.ContractItems)} can't be  empty.");

        return _contract;
    }
}