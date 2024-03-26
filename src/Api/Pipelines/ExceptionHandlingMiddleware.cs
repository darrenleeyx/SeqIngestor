using Common.Constants;
using Common.Logging;
using Contract.Common.Responses;

namespace Api.Pipelines;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILoggerAdapter<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILoggerAdapter<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaType.JSON;

            await context.Response.WriteAsJsonAsync(new ErrorResponse
            {
                Message = ErrorMessage.InternalServerError
            });
        }
    }
}