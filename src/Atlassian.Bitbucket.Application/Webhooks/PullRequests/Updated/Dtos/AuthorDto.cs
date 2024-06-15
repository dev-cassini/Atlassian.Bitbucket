using System.Text.Json.Serialization;

namespace Atlassian.Bitbucket.Application.Webhooks.PullRequests.Updated.Dtos;

public record AuthorDto(
    string Uuid, 
    [property: JsonPropertyName("display_name")] string DisplayName);