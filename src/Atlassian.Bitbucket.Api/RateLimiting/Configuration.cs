namespace Atlassian.Bitbucket.Api.RateLimiting;

public class Configuration
{
    public required int PermitLimit { get; init; }
    public required int QueueLimit { get; init; }
}