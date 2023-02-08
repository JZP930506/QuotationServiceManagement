using Microsoft.AspNetCore.Mvc;

namespace QuotationServiceManagement.Application.Web.Controllers;

public abstract class CommonControllerBase : ControllerBase
{
    protected virtual IActionResult Succeed<T>(T data)
    {
        var response = Succeed(data, StatusCodes.Status200OK);

        return response;
    }

    protected virtual IActionResult Succeed<T>(T data, string message)
    {
        var response = Succeed(data, StatusCodes.Status200OK, message);

        return response;
    }

    protected virtual IActionResult Succeed<T>(T data, int httpStatusCode)
    {
        var response = Succeed(data, httpStatusCode, ApiMessages.SUCCEED);

        return response;
    }

    protected virtual IActionResult Succeed<T>(T data, int httpStatusCode, string message)
    {
        var response = GenericResponse(data, message, httpStatusCode);

        return response;
    }

    public static IActionResult GenericResponse<T>(T data, string message,
        int httpStatusCode)
    {
        return new JsonResult(new GenericResponse<T>
        {
            Message = message,
            Data = data
        }, JsonSerializerOptionsFactory.GetCommonOptions())
        {
            StatusCode = httpStatusCode
        };
    }
}