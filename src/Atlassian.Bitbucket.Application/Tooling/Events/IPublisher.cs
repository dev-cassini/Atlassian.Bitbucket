namespace Atlassian.Bitbucket.Application.Tooling.Events;

public interface IPublisher
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class;
}