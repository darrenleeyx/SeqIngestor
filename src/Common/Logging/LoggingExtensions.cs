using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Logging;

public static class LoggingExtensions
{
    public static void OptionsValidationFailure(this ILogger logger, OptionsValidationException ex)
    {
        logger.LogError(exception: ex, message: "One or more validation error(s) occurred in appsettings.json");
    }
}
