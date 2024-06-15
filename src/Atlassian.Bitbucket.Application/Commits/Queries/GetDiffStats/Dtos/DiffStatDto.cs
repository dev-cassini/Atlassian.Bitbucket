using System.Text.Json.Serialization;

namespace Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats.Dtos;

public record DiffStatDto(
    [property: JsonPropertyName("lines_added")] int LinesAdded,
    [property: JsonPropertyName("lines_removed")] int LinesRemoved,
    string Status);