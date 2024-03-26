using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Logging;

public class LoggerAdapter<T> : ILoggerAdapter<T> where T : class
{
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string? message, params object?[] args) => _logger.LogInformation(message, args);
    public void LogWarning(string? message, params object?[] args) => _logger.LogWarning(message, args);
    public void LogError(Exception? exception, string? message, params object?[] args) => _logger.LogError(exception, message, args);

    public void OptionsValidationFailure(OptionsValidationException exception) => _logger.OptionsValidationFailure(exception);

}
