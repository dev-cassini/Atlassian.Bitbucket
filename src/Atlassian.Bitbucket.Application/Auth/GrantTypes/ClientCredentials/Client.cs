namespace Atlassian.Bitbucket.Application.Auth.GrantTypes.ClientCredentials;

public class Client
{
    public required string Id { get; init; }
    public required string Secret { get; init; }
}