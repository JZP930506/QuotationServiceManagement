namespace QuotationServiceManagement.Application.Infrastructure.Exceptions
{
    public class AppException : Exception
    {
        public AppException(int errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }
    }
}