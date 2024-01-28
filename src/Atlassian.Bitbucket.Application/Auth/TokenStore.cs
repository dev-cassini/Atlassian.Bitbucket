namespace Atlassian.Bitbucket.Application.Auth;

public class TokenStore : ITokenStore
{
    public Dictionary<string, Token> Tokens { get; } = new();
}