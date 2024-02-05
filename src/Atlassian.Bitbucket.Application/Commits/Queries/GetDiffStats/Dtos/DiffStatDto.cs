using Newtonsoft.Json;

namespace Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats.Dtos;

public record DiffStatDto(
    [JsonProperty("lines_added")] int LinesAdded,
    [JsonProperty("lines_removed")] int LinesRemoved,
    string Status);