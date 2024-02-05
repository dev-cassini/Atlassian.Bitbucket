using MediatR;

namespace Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats;

/// <summary>
/// Get commit diff statistics.
/// </summary>
/// <param name="RepositoryId">Unique identifier of the repository to which the commit belongs.</param>
/// <param name="CommitHash">Commit hash.</param>
public record Query(string RepositoryId, string CommitHash) : IRequest<Response>;