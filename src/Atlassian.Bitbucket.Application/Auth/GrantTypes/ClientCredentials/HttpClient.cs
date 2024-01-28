using Newtonsoft.Json;

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

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var token = JsonConvert.DeserializeObject<Token>(responseContent);
        if (token is null)
        {
            throw new Exception();
        }
        
        tokenStore.Tokens.Clear();
        tokenStore.Tokens.Add("Default", token);
        
        return token;
    }
}