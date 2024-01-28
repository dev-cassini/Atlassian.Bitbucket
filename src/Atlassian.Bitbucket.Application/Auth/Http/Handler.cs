using System.Net.Http.Headers;

namespace Atlassian.Bitbucket.Application.Auth.Http;

public class Handler(
    ITokenStore tokenStore, 
    GrantTypes.ClientCredentials.IHttpClient httpClient) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (tokenStore.Tokens.Any() is false)
        {
            await httpClient.RequestTokenAsync(cancellationToken);
        }

        var header = new AuthenticationHeaderValue("Bearer", tokenStore.Tokens.First().Value.AccessToken);
        request.Headers.Authorization = header;
        var response = await base.SendAsync(request, cancellationToken);
        
        return response;
    }
}