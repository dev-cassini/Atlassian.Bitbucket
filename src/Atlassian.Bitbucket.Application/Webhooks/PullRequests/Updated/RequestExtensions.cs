using Atlassian.Bitbucket.Domain.Events;

namespace Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated;

internal static class RequestExtensions
{
    internal static PullRequestUpdated ToDomainEvent(this Request request)
    {
        return new PullRequestUpdated(
            request.PullRequest.Id,
            request.Repository.Uuid,
            request.PullRequest.Author.Uuid,
            request.PullRequest.CommentCount,
            request.PullRequest.TaskCount,
            request.PullRequest.State,
            request.PullRequest.MergeCommit.Hash,
            request.PullRequest.CreatedOn,
            request.PullRequest.UpdatedOn);
    }
}