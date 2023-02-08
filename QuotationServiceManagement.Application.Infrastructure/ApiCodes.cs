namespace QuotationServiceManagement.Application.Infrastructure;

public static class ApiCodes
{
    /// <summary>
    ///     Success
    /// </summary>
    public const int API_SUCCEED_CODE = 20000;

    /// <summary>
    ///     Authentication failed
    /// </summary>
    public const int API_AUTH_FAILED_CODE = 30000;

    /// <summary>
    ///     Business failed
    /// </summary>
    public const int API_BUSINESS_FAILED_CODE = 40000;

    /// <summary>
    ///     Not Found Data
    /// </summary>z
    public const int API_NOT_FOUND_CODE = 40004;

    /// <summary>
    ///     Unexpected error
    /// </summary>
    public const int API_UNEXPECTED_FAILED_CODE = 50000;

    /// <summary>
    ///     Client error
    /// </summary>
    public const int API_Client_FAILED_CODE = 60000;
}