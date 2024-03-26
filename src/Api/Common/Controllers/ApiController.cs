using Api.Common.Models.ActionResults;
using Common.Constants;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Common.Controllers;

[ApiController]
[Consumes(MediaType.JSON)]
[Produces(MediaType.JSON)]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return base.Problem();
        }

        var firstError = errors.First();

        if (errors.Count > 1 && errors.All(error => error.Type == firstError.Type))
        {
            return ProblemsResponse(errors);
        }

        return ProblemResponse(firstError);
    }

    protected IActionResult ProblemsResponse(List<Error> errors)
    {
        ModelStateDictionary modelState = new ModelStateDictionary();
        foreach (var error in errors)
        {
            if (error.Code is not null && error.Description is not null)
            {
                modelState.AddModelError(
                    key: error.Code,
                    errorMessage: error.Description);
            }
        }

        var statusCode = MapToHttpStatusCode(errors.First());
        var message = MapToMessage(statusCode);

        return new ProblemResult(
            statusCode: statusCode,
            message: message,
            modelState: modelState);
    }

    protected IActionResult ProblemResponse(Error error)
    {
        ModelStateDictionary? modelState = null;
        if (error.Code is not null && error.Description is not null)
        {
            modelState = new ModelStateDictionary();
            modelState.AddModelError(
                key: error.Code,
                errorMessage: error.Description);
        }

        int statusCode = MapToHttpStatusCode(error);

        return new ProblemResult(
            statusCode: MapToHttpStatusCode(error),
            message: MapToMessage(statusCode),
            modelState: modelState);
    }

    private int MapToHttpStatusCode(Error error)
    {
        return (int)error.Type switch
        {
            (int)ErrorType.Failure => StatusCodes.Status500InternalServerError,
            (int)ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
            (int)ErrorType.Validation => StatusCodes.Status400BadRequest,
            (int)ErrorType.Conflict => StatusCodes.Status409Conflict,
            (int)ErrorType.NotFound => StatusCodes.Status404NotFound,
            (int)ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            (int)ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            (int)CustomErrorType.BadGateway => StatusCodes.Status502BadGateway,
            _ => StatusCodes.Status500InternalServerError,
        };
    }

    private string MapToMessage(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => ErrorMessage.Validation,
            StatusCodes.Status401Unauthorized => ErrorMessage.Unauthorized,
            StatusCodes.Status403Forbidden => ErrorMessage.Forbidden,
            StatusCodes.Status404NotFound => ErrorMessage.NotFound,
            StatusCodes.Status409Conflict => ErrorMessage.Conflict,
            StatusCodes.Status500InternalServerError => ErrorMessage.InternalServerError,
            StatusCodes.Status502BadGateway => ErrorMessage.BadGateway,
            _ => throw new InvalidOperationException($"Unable to map status code ({statusCode}) to an error message.")
        };
    }
}