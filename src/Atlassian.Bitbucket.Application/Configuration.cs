namespace Atlassian.Bitbucket.Application;

public class Configuration
{
    public required string OAuthBaseUrl { get; init; }
    public required string ApiBaseUrl { get; init; }
}