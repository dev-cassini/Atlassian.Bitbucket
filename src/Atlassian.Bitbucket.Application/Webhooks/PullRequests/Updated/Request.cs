using Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated.Dtos;
using MediatR;
using Newtonsoft.Json;

namespace Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated;

public record Request(
    RepositoryDto Repository,
    [JsonProperty("pull_request")] PullRequestDto PullRequest) : IRequest;