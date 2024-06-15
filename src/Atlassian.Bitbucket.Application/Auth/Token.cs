using System.Text.Json.Serialization;

namespace Atlassian.Bitbucket.Application.Auth;

public record Token(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("expires_in")] int ExpiresInSeconds);