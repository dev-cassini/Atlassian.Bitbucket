using IdentityModel;

namespace Atlassian.Bitbucket.Application.Auth.GrantTypes.ClientCredentials;

public interface IHttpClient
{
    /// <summary>
    /// Request a token using <see cref="OidcConstants.GrantTypes.ClientCredentials"/> flow.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="Token"/> from auth server.</returns>
    Task<Token> RequestTokenAsync(CancellationToken cancellationToken);
}