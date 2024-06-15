using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Atlassian.Bitbucket.Application.Auth.GrantTypes.ClientCredentials;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddBitbucketClient(
        this IServiceCollection serviceCollection,
        Action<Client> clientAction)
    {
        serviceCollection
            .AddOptions<Client>()
            .Configure(clientAction);
        
        return serviceCollection;
    }
    
    internal static IServiceCollection AddBitbucketClientCredentialsHttpClient(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IHttpClient, HttpClient>((serviceProvider, httpClient) =>
        {
            var bitbucket = serviceProvider.GetRequiredService<IOptions<Configuration>>().Value;
            httpClient.BaseAddress = new Uri(bitbucket.OAuthBaseUrl);

            var bitbucketClient = serviceProvider.GetRequiredService<IOptions<Client>>().Value;
            httpClient.SetBasicAuthentication(bitbucketClient.Id, bitbucketClient.Secret);
        });

        return serviceCollection;
    }
}