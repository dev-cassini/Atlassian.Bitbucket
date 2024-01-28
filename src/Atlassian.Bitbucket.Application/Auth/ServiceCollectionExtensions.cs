using Atlassian.Bitbucket.Application.Auth.GrantTypes;
using Atlassian.Bitbucket.Application.Auth.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Application.Auth;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddAuthServices(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddHttpMessageHandlers()
            .AddTokenStore()
            .AddClientCredentialsServices(configuration);
        
        return serviceCollection;
    }
    
    private static IServiceCollection AddTokenStore(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITokenStore, TokenStore>();
        
        return serviceCollection;
    }
}