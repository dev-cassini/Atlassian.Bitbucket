using Atlassian.Bitbucket.Api.Endpoints;
using Atlassian.Bitbucket.Application;
using Atlassian.Bitbucket.Infrastructure;
using Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit.Transports.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
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
    .UseHttpsRedirection();

app.Run();

public partial class Program;