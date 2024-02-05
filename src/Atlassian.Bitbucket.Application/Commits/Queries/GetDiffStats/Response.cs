using Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats.Dtos;

namespace Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats;

public record Response(IEnumerable<DiffStatDto> Values);