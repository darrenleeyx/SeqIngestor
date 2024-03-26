using Common.Constants;
using Contract.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Pipelines;

public class InvalidJsonFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is BadRequestObjectResult objectResult)
        {
            if (objectResult.Value is ValidationProblemDetails)
            {
                context.Result = new BadRequestObjectResult(new ErrorResponse
                {
                    Message = ErrorMessage.InvalidJson
                });
                return;
            }
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}