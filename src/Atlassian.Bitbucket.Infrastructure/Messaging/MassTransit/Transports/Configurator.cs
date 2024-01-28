using Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit.Transports.Azure;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit.Transports;

public class Configurator(
    IServiceCollection serviceCollection,
    IBusRegistrationConfigurator busRegistrationConfigurator)
{
    public Configurator UsingAzureServiceBus(Action<ServiceBus> serviceBusAction)
    {
        serviceCollection
            .AddOptions<ServiceBus>()
            .Configure(serviceBusAction);
        
        busRegistrationConfigurator.UsingAzureServiceBus();
        return this;
    }
}