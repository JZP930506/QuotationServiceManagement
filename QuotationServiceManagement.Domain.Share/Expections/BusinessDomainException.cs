namespace QuotationServiceManagement.Domain.Share.Expections
{
    public class BusinessDomainException : DomainException
    {
        public BusinessDomainException()
        {
        }

        public BusinessDomainException(string message)
            : base(message)
        {
        }

        public BusinessDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}