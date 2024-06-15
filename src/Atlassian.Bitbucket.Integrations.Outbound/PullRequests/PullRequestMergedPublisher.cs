using Atlassian.Bitbucket.Domain.Enums;
using Atlassian.Bitbucket.Domain.Events;
using Atlassian.Bitbucket.Integrations.Outbound.Notifications.PullRequests;
using MediatR;
using IPublisher = Atlassian.Bitbucket.Application.Tooling.Events.IPublisher;

namespace Atlassian.Bitbucket.Integrations.Outbound.PullRequests;

using GetDiffStats = Application.Commits.Queries.GetDiffStats;

public class PullRequestMergedPublisher(
    IPublisher publisher,
    ISender mediator) : INotificationHandler<PullRequestUpdated>
{
    public async Task Handle(PullRequestUpdated notification, CancellationToken cancellationToken)
    {
        if (notification.State is not PullRequestStates.Merged)
        {
            return;
        }

        var request = new GetDiffStats.Query(notification.RepositoryId, notification.MergeCommitHash!);
        var response = await mediator.Send(request, cancellationToken);
        
        var @event = new PullRequestMerged(
            notification.Id,
            notification.RepositoryId,
            notification.AuthorId,
            notification.CommentCount,
            notification.TaskCount,
            response.Values.Sum(x => x.LinesAdded),
            response.Values.Sum(x => x.LinesRemoved),
            response.Values.Count(x => x.Status == CommitDiffStatStates.Added),
            response.Values.Count(x => x.Status == CommitDiffStatStates.Modified),
            response.Values.Count(x => x.Status == CommitDiffStatStates.Removed),
            notification.State,
            notification.MergeCommitHash,
            notification.CreatedOn,
            notification.UpdatedOn);

        await publisher.PublishAsync(@event, cancellationToken);
    }
}