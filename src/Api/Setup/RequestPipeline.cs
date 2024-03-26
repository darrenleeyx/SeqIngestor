using Api.Pipelines;
using Common.Options.Swagger;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Api.Setup;

public static class RequestPipeline
{
    public static IMvcBuilder AddFilters(this IMvcBuilder builder)
    {
        builder.AddMvcOptions(options =>
        {
            options.Filters.Add(new ValidationFilterAttribute());
            options.Filters.Add(new InvalidJsonFilter());
        });

        return builder;
    }

    public static IApplicationBuilder UseApi(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.UseAuthorization();

        return app;
    }

    public static IApplicationBuilder UseSwagger(this IApplicationBuilder application)
    {
        var swaggerOptions = application.ApplicationServices.GetRequiredService<IOptions<SwaggerOptions>>().Value;

        var servers = new List<OpenApiServer>();

        if (!string.IsNullOrEmpty(swaggerOptions.DevelopmentUrl))
        {
            servers.Add(new OpenApiServer
            {
                Url = swaggerOptions.DevelopmentUrl,
                Description = "Development Server"
            });
        }

        if (!string.IsNullOrEmpty(swaggerOptions.ProductionUrl))
        {
            servers.Add(new OpenApiServer
            {
                Url = swaggerOptions.ProductionUrl,
                Description = "Production Server"
            });
        }

        application
            .UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swagger, httpRequest) =>
                {
                    swagger.Servers = servers;
                });
            })
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Seq Ingestor API v1");
            });

        return application;
    }

    public static IEndpointRouteBuilder MapApi(this IEndpointRouteBuilder app)
    {
        app.MapSwagger();
        //app.MapHealthChecks("/health", HealthCheckHelper.CreateOptions());
        app.MapControllers();

        return app;
    }
}
