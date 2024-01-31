namespace Atlassian.Bitbucket.Domain.Events;

public record PullRequestUpdated(
    int Id, 
    string RepositoryId, 
    string AuthorId, 
    int CommentCount, 
    int TaskCount, 
    string State, 
    string? MergeCommitHash, 
    DateTimeOffset CreatedOn, 
    DateTimeOffset UpdatedOn);