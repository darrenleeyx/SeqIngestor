using Api.Common.Models.EnvironmentVariables;

namespace Api.Common.Extensions;

public static class EnvironmentVariableExtensions
{
    public static IConfigurationBuilder AddEnvironmentVariablesWithKeyTransform(this IConfigurationBuilder builder, string prefix)
    {
        builder.Add(new EnvironmentVariableConfigurationSource(prefix));

        return builder;
    }
}