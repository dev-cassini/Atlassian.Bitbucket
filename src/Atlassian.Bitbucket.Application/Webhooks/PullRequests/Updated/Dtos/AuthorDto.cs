using Newtonsoft.Json;

namespace Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated.Dtos;

public record AuthorDto(
    string Uuid, 
    [JsonProperty("display_name")] string DisplayName);