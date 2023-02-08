namespace QuotationServiceManagement.Domain.Share.Expections
{
    public class ValidationDomainException : DomainException
    {
        public ValidationDomainException()
        {
        }

        public ValidationDomainException(string message)
            : base(message)
        {
        }

        public ValidationDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}