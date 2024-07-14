namespace Atlassian.Bitbucket.Api.RateLimiting;

public class Configuration
{
    public required int FixedWindowPermitLimit { get; init; }
    public required int FixedWindowQueueLimit { get; init; }
    public required int ConcurrentPermitLimit { get; init; }
    public required int ConcurrentQueueLimit { get; init; }
}