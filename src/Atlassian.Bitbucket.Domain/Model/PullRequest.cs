namespace Atlassian.Bitbucket.Domain.Model;

public class PullRequest(
    int id,
    string repositoryId,
    string authorId,
    int commentCount,
    int taskCount,
    string state,
    string? mergeCommitHash,
    DateTimeOffset createdOn,
    DateTimeOffset updatedOn) : Entity
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public int Id { get; } = id;

    /// <summary>
    /// Unique identifier of repository the pull request is associated with.
    /// </summary>
    public string RepositoryId { get; } = repositoryId;

    /// <summary>
    /// Unique identifier of the author.
    /// </summary>
    public string AuthorId { get; } = authorId;

    /// <summary>
    /// Total number of comments on the pull request.
    /// </summary>
    public int CommentCount { get; } = commentCount;

    /// <summary>
    /// Total number of tasks on the pull request.
    /// </summary>
    public int TaskCount { get; } = taskCount;

    /// <summary>
    /// Current state.
    /// </summary>
    public string State { get; } = state;

    /// <summary>
    /// Unique identifier of the merge commit, if the pull request is merged.
    /// </summary>
    public string? MergeCommitHash { get; } = mergeCommitHash;

    /// <summary>
    /// When the pull request was created.
    /// </summary>
    public DateTimeOffset CreatedOn { get; } = createdOn;

    /// <summary>
    /// When the pull request was last updated.
    /// </summary>
    public DateTimeOffset UpdatedOn { get; } = updatedOn;
}