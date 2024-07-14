using Atlassian.Bitbucket.Api.Endpoints;
using Atlassian.Bitbucket.Api.RateLimiting;
using Atlassian.Bitbucket.Application;
using Atlassian.Bitbucket.Infrastructure;
using Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit.Transports.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddRateLimiter(
        builder.Configuration,
        rateLimiterOptions => rateLimiterOptions.ConfigureGlobalRateLimiter())
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructure(configurator =>
    {
        configurator.AddMessaging(messagingConfigurator =>
        {
            messagingConfigurator
                .AddMassTransit(massTransitConfigurator =>
                {
                    massTransitConfigurator.UsingAzureServiceBus(
                        builder.Configuration
                            .GetSection(nameof(Azure))
                            .GetSection(nameof(ServiceBus))
                            .Bind);
                })
                .AddMediatR();
        });
    });

builder.Services.AddHealthChecks();

var app = builder.Build();

app
    .RegisterEndpoints()
    .UseRateLimiter()
    .UseHttpsRedirection()
    .UseHealthChecks(new PathString("/health"));

app.Run();

public partial class Program;