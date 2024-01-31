using MediatR;
using IPublisher = Atlassian.Bitbucket.Application.Tooling.Events.IPublisher;

namespace Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated;

public class RequestHandler(IPublisher publisher) : IRequestHandler<Request>
{
    public async Task Handle(Request request, CancellationToken cancellationToken)
    {
        var @event = request.ToDomainEvent();
        await publisher.PublishAsync(@event, cancellationToken);
    }
}