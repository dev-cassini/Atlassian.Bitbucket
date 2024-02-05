using Newtonsoft.Json;

namespace Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated.Dtos;

public record PullRequestDto(
    int Id,
    [JsonProperty("comment_count")] int CommentCount,
    [JsonProperty("task_count")] int TaskCount,
    string State,
    [JsonProperty("merge_commit")] MergeCommitDto? MergeCommit,
    AuthorDto Author,
    [JsonProperty("created_on")] DateTimeOffset CreatedOn,
    [JsonProperty("updated_on")] DateTimeOffset UpdatedOn);