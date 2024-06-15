using System.Text.Json;

namespace Atlassian.Bitbucket.Application.Auth.GrantTypes.ClientCredentials;

public class HttpClient(System.Net.Http.HttpClient httpClient, ITokenStore tokenStore) : IHttpClient
{
    public async Task<Token> RequestTokenAsync(CancellationToken cancellationToken)
    {
        var requestContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
        {
            new("grant_type", "client_credentials")
        });
        
        var response = await httpClient.PostAsync(
            "access_token",
            requestContent,
            cancellationToken);

        var responseContentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var token = await JsonSerializer.DeserializeAsync<Token>(responseContentStream, cancellationToken: cancellationToken);
        if (token is null)
        {
            throw new Exception("Failed to deserialize token response.");
        }
        
        tokenStore.Tokens.Clear();
        tokenStore.Tokens.Add("Default", token);
        
        return token;
    }
}