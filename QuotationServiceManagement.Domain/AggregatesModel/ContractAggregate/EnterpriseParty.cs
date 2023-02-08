namespace QuotationServiceManagement.Domain.AggregatesModel.ContractAggregate;

public class EnterpriseParty : Entity
{
    public EnterpriseParty(string title, string address, LinkInfo linkInfo, BankInfo bankInfo)
    {
        Title = title;
        Address = address;
        LinkInfo = linkInfo;
        BankInfo = bankInfo;
    }

    public string Title { get; set; }

    public string Address { get; set; }

    public LinkInfo LinkInfo { get; set; }

    public BankInfo BankInfo { get; set; }
}