using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Setup;

public static class DependencyInjection
{
    public static IServiceCollection AddContract(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(IContractMarker).Assembly);

        return services;
    }
}
