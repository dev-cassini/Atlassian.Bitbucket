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
            .AddMassTransit(configurator =>
            {
                var transportConfigurator = new Transports.Configurator(serviceCollection, configurator);
                transportConfiguratorAction.Invoke(transportConfigurator);
            });
        
        return serviceCollection;
    }
}