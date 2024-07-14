using Atlassian.Bitbucket.Application.Tooling.Events;
using MassTransit;

namespace Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit;

public class EventPublisher(IPublishEndpoint bus) : IEventPublisher
{
    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) 
        where T : class
    {
        await bus.Publish(message, cancellationToken);
    }
}