using Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit;
using Atlassian.Bitbucket.Infrastructure.Messaging.MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Infrastructure.Messaging;

public class Configurator(IServiceCollection serviceCollection)
{
    public Configurator AddMassTransit(Action<MassTransit.Transports.Configurator> configuratorAction)
    {
        serviceCollection.AddMassTransit(configuratorAction);
        return this;
    }

    public Configurator AddMediatR()
    {
        serviceCollection.AddMediatR();
        return this;
    }
}