using System.Text.Json.Serialization;

namespace Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated.Dtos;

public record PullRequestDto(
    int Id,
    [property: JsonPropertyName("comment_count")] int CommentCount,
    [property: JsonPropertyName("task_count")] int TaskCount,
    string State,
    [property: JsonPropertyName("merge_commit")] MergeCommitDto? MergeCommit,
    AuthorDto Author,
    [property: JsonPropertyName("created_on")] DateTimeOffset CreatedOn,
    [property: JsonPropertyName("updated_on")] DateTimeOffset UpdatedOn);