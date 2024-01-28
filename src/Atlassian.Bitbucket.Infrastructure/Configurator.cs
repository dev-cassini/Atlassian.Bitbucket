using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Infrastructure;

public class Configurator(IServiceCollection serviceCollection)
{
    public Configurator AddMessaging(Action<Messaging.Configurator> configuratorAction)
    {
        var configurator = new Messaging.Configurator(serviceCollection);
        configuratorAction.Invoke(configurator);
        
        return this;
    }
}