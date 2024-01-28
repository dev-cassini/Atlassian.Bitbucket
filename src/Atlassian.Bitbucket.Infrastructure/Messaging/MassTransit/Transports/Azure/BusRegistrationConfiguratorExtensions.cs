using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit.Transports.Azure;

internal static class BusRegistrationConfiguratorExtensions
{
    internal static IBusRegistrationConfigurator UsingAzureServiceBus(
        this IBusRegistrationConfigurator busRegistrationConfigurator)
    {
        busRegistrationConfigurator
            .UsingAzureServiceBus((context, factoryConfigurator) =>
            {
                var serviceBus = context.GetRequiredService<IOptions<ServiceBus>>().Value;
                factoryConfigurator.Host(serviceBus.ConnectionString);
                factoryConfigurator.ConfigureEndpoints(context);
            });
        
        return busRegistrationConfigurator;
    }
}