using QuotationServiceManagement.Domain.Interface;

namespace QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.InquiryPartyAggregate
{
    public class InquiryParty : Entity, IHasGcid, IAggregateRoot
    {
        private InquiryParty()
        {
            Gcid = Guid.NewGuid().ToString();
        }

        public InquiryParty(string title, string address, LinkInfo linkInfo, BankInfo bankInfo) : this()
        {
            Title = title;
            Address = address;
            LinkInfo = linkInfo;
            BankInfo = bankInfo;
        }

        public string Gcid { get; }

        public string Title { get; }

        public string Address { get; }

        public LinkInfo LinkInfo { get; set; }

        public BankInfo BankInfo { get; set; }


        public InquiryParty ChangeBankInfo(BankInfo bankInfo)
        {
            BankInfo = bankInfo;
            return this;
        }

        public InquiryParty ChangeLinkInfo(LinkInfo linkInfo)
        {
            LinkInfo = linkInfo;
            return this;
        }
    }
}