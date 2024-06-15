using Atlassian.Bitbucket.Application.Auth.GrantTypes.ClientCredentials;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Application.Auth.GrantTypes;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddClientCredentialsServices(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddBitbucketClient(configuration
                .GetSection(nameof(Bitbucket))
                .GetSection(nameof(Client))
                .Bind)
            .AddBitbucketClientCredentialsHttpClient();
        
        return serviceCollection;
    }
}