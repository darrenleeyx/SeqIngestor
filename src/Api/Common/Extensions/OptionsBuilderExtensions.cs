using Api.Common.Models.FluentValidation;
using Microsoft.Extensions.Options;

namespace Api.Common.Extensions;

public static class OptionsBuilderExtensions
{
    public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(
        this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(provider =>
            new FluentValidationOptions<TOptions>(optionsBuilder.Name, provider));

        return optionsBuilder;
    }
}