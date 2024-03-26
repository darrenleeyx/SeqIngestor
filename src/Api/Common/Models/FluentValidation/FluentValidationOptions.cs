using Common.Logging;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;

namespace Api.Common.Models.FluentValidation;

public class FluentValidationOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
{
    private readonly string? _name;
    private readonly IServiceProvider _serviceProvider;

    public FluentValidationOptions(string? name, IServiceProvider serviceProvider)
    {
        _name = name;
        _serviceProvider = serviceProvider;
    }

    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (_name != null && _name != name)
        {
            return ValidateOptionsResult.Skip;
        }

        using var scope = _serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerAdapter<FluentValidationOptions<TOptions>>>();

        try
        {
            ArgumentNullException.ThrowIfNull(options);

            var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

            ValidationResult results = validator.Validate(options);

            if (results.IsValid)
            {
                return ValidateOptionsResult.Success;
            }

            Type type = options.GetType();

            List<string> errors = [];

            foreach (var error in results.Errors)
            {
                errors.Add($"{error.PropertyName}: {error.ErrorMessage}");
            }

            var exception = new OptionsValidationException(type.Name, type, errors);
            logger.OptionsValidationFailure(exception);

            return ValidateOptionsResult.Fail(errors);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
