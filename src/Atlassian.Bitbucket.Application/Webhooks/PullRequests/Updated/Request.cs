using System.Text.Json.Serialization;
using Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated.Dtos;
using MediatR;

namespace Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated;

public record Request(
    RepositoryDto Repository,
    [property: JsonPropertyName("pull_request")] PullRequestDto PullRequest) : IRequest;