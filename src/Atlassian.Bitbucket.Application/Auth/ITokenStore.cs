namespace Atlassian.Bitbucket.Application.Auth;

public interface ITokenStore
{
    Dictionary<string, Token> Tokens { get; }
}