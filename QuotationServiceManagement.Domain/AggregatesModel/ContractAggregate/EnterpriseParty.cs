namespace QuotationServiceManagement.Domain.AggregatesModel.ContractAggregate
{
    public class EnterpriseParty : ValueObject
    {
        public EnterpriseParty()
        {
            
        }
        
        public EnterpriseParty(string title, string address, string linkMan, string email, string phone, string fax, string bankAccount, string openingBank)
            :this()
        {
            Title = title;
            Address = address;
            LinkMan = linkMan;
            Email = email;
            Phone = phone;
            Fax = fax;
            BankAccount = bankAccount;
            OpeningBank = openingBank;
        }

        public string Title { get; set; }

        public string Address { get; set; }

        public string LinkMan { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }
        
        public string BankAccount { get; set; }

        public string OpeningBank { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;
            yield return Address;
            yield return LinkMan;
            yield return Fax;
            yield return Email;
            yield return Phone;
            yield return BankAccount;
            yield return OpeningBank;
        }
    }
    
}