using Api.Common.Extensions;
using Common.Constants;
using Contract.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Common.Models.ActionResults;

public class ProblemResult : IActionResult
{
    private readonly int _statusCode;
    private readonly string _message;
    private readonly ModelStateDictionary? _modelState;

    public ProblemResult(int statusCode, string message, ModelStateDictionary? modelState = null)
    {
        _statusCode = statusCode;
        _message = message;
        _modelState = modelState;
    }
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var errorResponse = new ErrorResponse
        {
            Message = _message,
            Errors = _modelState is not null ?
                _modelState.ToErrorDictionary() :
                new Dictionary<string, List<string>>()
        };
        context.HttpContext.Response.StatusCode = _statusCode;
        context.HttpContext.Response.ContentType = MediaType.JSON;

        await context.HttpContext.Response.WriteAsJsonAsync(errorResponse);
    }
}