
using Microsoft.Extensions.Options;

namespace Common.Logging;

public interface ILoggerAdapter<T> where T : class
{
    void LogError(Exception? exception, string? message, params object?[] args);
    void LogInformation(string? message, params object?[] args);
    void LogWarning(string? message, params object?[] args);

    void OptionsValidationFailure(OptionsValidationException exception);
}