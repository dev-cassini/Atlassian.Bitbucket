using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Application.Auth.Http;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddHttpMessageHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<Handler>();
        return serviceCollection;
    }
}