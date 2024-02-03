using Atlassian.Bitbucket.Application.Tooling.Events;
using Atlassian.Bitbucket.Domain.Events;
using Atlassian.Bitbucket.Integrations.Outbound.Notifications.PullRequests;
using MassTransit;

namespace Atlassian.Bitbucket.Integrations.Outbound.PullRequests;

public class PullRequestMergedSender(IPublisher publisher) : IConsumer<PullRequestUpdated>
{
    public async Task Consume(ConsumeContext<PullRequestUpdated> context)
    {
        if (context.Message.State is not "MERGED")
        {
            return;
        }
        
        var @event = new PullRequestMerged(
            context.Message.Id,
            context.Message.RepositoryId,
            context.Message.AuthorId,
            context.Message.CommentCount,
            context.Message.TaskCount,
            1,
            1,
            1,
            1,
            1,
            context.Message.State,
            context.Message.MergeCommitHash,
            context.Message.CreatedOn,
            context.Message.UpdatedOn);

        await publisher.PublishAsync(@event, context.CancellationToken);
    }
}