using QuotationServiceManagement.Application.Infrastructure;

namespace QuotationServiceManagement.Application.Web;

public static class ApiCodeMap
{
    public static Dictionary<int, int> Maps { get; } =
        new Dictionary<int, int>
        {
            { ApiCodes.API_SUCCEED_CODE, StatusCodes.Status200OK},
            { ApiCodes.API_AUTH_FAILED_CODE, StatusCodes.Status401Unauthorized },
            { ApiCodes.API_NOT_FOUND_CODE, StatusCodes.Status404NotFound },
            { ApiCodes.API_BUSINESS_FAILED_CODE, StatusCodes.Status405MethodNotAllowed },
            { ApiCodes.API_UNEXPECTED_FAILED_CODE, StatusCodes.Status500InternalServerError },
            { ApiCodes.API_Client_FAILED_CODE, StatusCodes.Status400BadRequest }
        };

    public static int GetHttpStatusCode(int errorCode)
    {
        return Maps.ContainsKey(errorCode) ? Maps[errorCode] : StatusCodes.Status500InternalServerError;
    }
}