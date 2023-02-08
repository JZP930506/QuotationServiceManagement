using QuotationServiceManagement.Domian.Share.Models;

namespace QuotationServiceManagement.Domain.Share.Models
{
    public class BankInfo : ValueObject
    {
        public BankInfo()
        { }

        public BankInfo(string bankAccount, string openingBank) : this()
        {
            BankAccount = bankAccount;
            OpeningBank = openingBank;
        }

        public string BankAccount { get; set; }

        public string OpeningBank { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return BankAccount;

            yield return OpeningBank;
        }
    }
}