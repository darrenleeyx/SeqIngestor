using Api.Common.Models.ActionResults;
using Common.Constants;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Pipelines;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new ProblemResult(
                statusCode: StatusCodes.Status400BadRequest,
                message: ErrorMessage.Validation,
                modelState: context.ModelState);
        }
    }
}

