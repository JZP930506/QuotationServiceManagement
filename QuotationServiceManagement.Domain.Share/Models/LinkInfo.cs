using QuotationServiceManagement.Domian.Share.Models;

namespace QuotationServiceManagement.Domain.Share.Models
{
    public class LinkInfo : ValueObject
    {
        public LinkInfo()
        {
        }

        public LinkInfo(string linkMan, string email, string phone, string fax) : this()
        {
            LinkMan = linkMan;
            Email = email;
            Phone = phone;
            Fax = fax;
        }

        public string LinkMan { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return LinkMan;
            yield return Email;
            yield return Phone;
            yield return Fax;
        }

    }
}