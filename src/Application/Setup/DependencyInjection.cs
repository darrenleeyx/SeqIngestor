using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Setup;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(IApplicationMarker).Assembly);

        services.Scan(scan => scan
            .FromAssemblyOf<IApplicationMarker>()
            .AddClasses(classes => classes
                .Where(type =>
                    type.Name.EndsWith("Service")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
