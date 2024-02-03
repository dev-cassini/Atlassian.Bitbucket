namespace Atlassian.Bitbucket.Integrations.Outbound.Notifications.PullRequests;

public record PullRequestMerged(
    int Id, 
    string RepositoryId, 
    string AuthorId, 
    int CommentCount, 
    int TaskCount, 
    int LinesAdded,
    int LinesRemoved,
    int FilesAdded,
    int FilesUpdated,
    int FilesRemoved,
    string State, 
    string? MergeCommitHash, 
    DateTimeOffset CreatedOn, 
    DateTimeOffset MergedOn);