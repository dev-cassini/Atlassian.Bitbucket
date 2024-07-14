namespace Atlassian.Bitbucket.Application.Tooling.Events;

public interface IEventPublisher
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class;
}