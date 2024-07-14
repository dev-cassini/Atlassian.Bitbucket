using Atlassian.Bitbucket.Application.Tooling.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddMassTransit(
        this IServiceCollection serviceCollection,
        Action<Transports.Configurator> transportConfiguratorAction)
    {
        serviceCollection
            .AddScoped<IEventPublisher, EventPublisher>()
            .AddMassTransit(configurator =>
            {
                var transportConfigurator = new Transports.Configurator(serviceCollection, configurator);
                transportConfiguratorAction.Invoke(transportConfigurator);
            });
        
        return serviceCollection;
    }
}