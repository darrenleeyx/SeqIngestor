using Common.Logging;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup;

public static class DependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

        services.AddValidatorsFromAssembly(typeof(ICommonMarker).Assembly);

        return services;
    }
}
