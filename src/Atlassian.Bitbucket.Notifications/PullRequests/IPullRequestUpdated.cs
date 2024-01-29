namespace Atlassian.Bitbucket.Notifications.PullRequests;

public interface IPullRequestUpdated
{
    public Guid Id { get; }
    
    public Guid RepositoryId { get; }
    
    public Guid AuthorId { get; }
    
    public int CommentCount { get; }
    
    public int TaskCount { get; }
    
    public string State { get; }

    public string? MergeCommitHash { get; }
    
    public DateTimeOffset CreatedOn { get; }
    
    public DateTimeOffset UpdatedOn { get; }
}