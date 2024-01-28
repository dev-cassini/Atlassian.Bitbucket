using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddMassTransit(
        this IServiceCollection serviceCollection,
        Action<Transports.Configurator> transportConfiguratorAction)
    {
        serviceCollection
            .AddMassTransit(configurator =>
            {
                var transportConfigurator = new Transports.Configurator(serviceCollection, configurator);
                transportConfiguratorAction.Invoke(transportConfigurator);
            });

        serviceCollection.AddHostedService<Worker>();
        
        return serviceCollection;
    }
}

public record GettingStarted
{
    public string Value { get; init; }
}

public class Worker : BackgroundService
{
    readonly IBus _bus;

    public Worker(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new GettingStarted { Value = $"The time is {DateTimeOffset.Now}" }, stoppingToken);

            await Task.Delay(5000, stoppingToken);
        }
    }
}