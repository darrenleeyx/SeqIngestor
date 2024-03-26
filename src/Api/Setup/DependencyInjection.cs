using Api.Common.Extensions;
using Api.Pipelines;
using Application.Services.Seq;
using Common.Options.Seq;
using Common.Options.Swagger;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.Setup;

public static class DependencyInjection
{
    public static IHostBuilder UseSerilog(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        hostBuilder.UseSerilog(Log.Logger);

        return hostBuilder;
    }


    public static IServiceCollection ConfigureFluentOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<SeqOptions>()
            .Bind(configuration.GetSection(SeqOptions.SectionName))
            .ValidateFluently()
            .ValidateOnStart();

        services
            .AddOptions<SwaggerOptions>()
            .Bind(configuration.GetSection(SwaggerOptions.SectionName))
            .ValidateFluently()
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddFilters()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        services
            .AddHealthChecks();
        // To add check for Seq later

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Seq Ingestor API", Version = "v1" });
            })
            .AddFluentValidationAutoValidation()
            .AddFluentValidationRulesToSwagger();

        return services;
    }

    public static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ExceptionHandlingMiddleware>();

        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddHttpClient(SeqConstants.Name, (serviceProvider, client) =>
        {
            var seqOptions = serviceProvider.GetRequiredService<IOptions<SeqOptions>>().Value;

            client.BaseAddress = new Uri(seqOptions.ServerUrl);
            client.DefaultRequestHeaders.Add(SeqConstants.ApiKeyHeaderName, seqOptions.ApiKey);
        });

        return services;
    }
}
