using Atlassian.Bitbucket.Application;
using Atlassian.Bitbucket.Infrastructure;
using Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit.Transports.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();