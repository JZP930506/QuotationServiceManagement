using System.Runtime.Serialization;

namespace QuotationServiceManagement.Application.Service.Quotation.DTOs
{
    [DataContract]
    public record InquiryPartyDto
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public string Title { get; private set; }

        [DataMember]
        public string Address { get; private set; }

        [DataMember]
        public string BankAccount { get; private set; }

        [DataMember]
        public string OpeningBank { get; private set; }

        [DataMember]
        public string LinkMan { get; private set; }

        [DataMember]
        public string Email { get; private set; }

        [DataMember]
        public string Phone { get; private set; }

        [DataMember]
        public string Fax { get; private set; }
    }
}