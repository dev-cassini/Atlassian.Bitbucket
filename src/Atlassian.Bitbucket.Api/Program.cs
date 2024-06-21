using Atlassian.Bitbucket.Api.Endpoints;
using Atlassian.Bitbucket.Api.RateLimiting.Policies;
using Atlassian.Bitbucket.Application;
using Atlassian.Bitbucket.Infrastructure;
using Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit.Transports.Azure;
using RateLimiting = Atlassian.Bitbucket.Api.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<RateLimiting.Configuration>()
    .Configure(builder.Configuration.GetSection(nameof(RateLimiting)).Bind);

builder.Services
    .AddEndpointsApiExplorer()
    .AddRateLimiter(rateLimiterOptions =>
    {
        rateLimiterOptions.AddPolicy<string, CustomSlidingWindowRateLimiter>("webhooks");
    })
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

var app = builder.Build();

app
    .RegisterEndpoints()
    .UseRateLimiter()
    .UseHttpsRedirection();

app.Run();

public partial class Program;