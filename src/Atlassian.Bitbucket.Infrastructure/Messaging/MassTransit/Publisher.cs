using Atlassian.Bitbucket.Application.Tooling.Events;
using MassTransit;

namespace Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit;

public class Publisher(IPublishEndpoint bus) : IPublisher
{
    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) 
        where T : class
    {
        await bus.Publish(message, cancellationToken);
    }
}