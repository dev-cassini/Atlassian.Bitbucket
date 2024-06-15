using Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated;
using Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated.Dtos;
using Atlassian.Bitbucket.Domain.Enums;

namespace Atlassian.Bitbucket.Builders.Application.Webhooks.PullRequests.Updated;

public class RequestBuilder
{
    private string _state = PullRequestStates.Open;
    
    public Request Build()
    {
        var utcNow = DateTimeOffset.UtcNow;
        return new Request(
            Repository: new RepositoryDto(Guid.NewGuid().ToString()),
            PullRequest: new PullRequestDto(
                Id: 1234,
                CommentCount: 1,
                TaskCount: 2,
                State: _state,
                MergeCommit: new MergeCommitDto(Guid.NewGuid().ToString()),
                Author: new AuthorDto(Guid.NewGuid().ToString(), "Test Author"),
                CreatedOn: utcNow,
                UpdatedOn: utcNow));
    }

    public RequestBuilder State(string state)
    {
        _state = state;
        return this;
    }
}