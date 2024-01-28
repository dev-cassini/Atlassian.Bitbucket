using Newtonsoft.Json;

namespace Atlassian.Bitbucket.Application.Auth;

public record Token(
    [JsonProperty("access_token")] string AccessToken,
    [JsonProperty("expires_in")] string ExpiresInSeconds);