using Api.Common.Constants;
using Api.Common.Extensions;
using Api.Setup;
using Application.Setup;
using Common.Setup;
using Contract.Setup;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    var configuration = builder.Configuration;

    builder.Configuration
            .AddEnvironmentVariablesWithKeyTransform(DataHolder.EnvironmentVariablePrefix)
            .Build();

    builder.Host
        .UseSerilog(configuration);

    services
        .AddCommon()
        .ConfigureFluentOptions(configuration)
        .AddContract()
        .AddApi()
        .AddApplication()
        .AddMiddlewares();

    services
        .AddHttpContextAccessor()
        .AddHttpClients();
}

var app = builder.Build();
{
    app.UseApi();

    app.UseSwagger();

    app.MapApi();

    app.Run();
}