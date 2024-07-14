using Microsoft.AspNetCore.RateLimiting;

namespace Atlassian.Bitbucket.Api.RateLimiting;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddRateLimiter(
        this IServiceCollection serviceCollection,
        IConfiguration configuration,
        Action<RateLimiterOptions> optionsAction)
    {
        serviceCollection
            .AddOptions<Configuration>()
            .Configure(configuration.GetSection(nameof(RateLimiting)).Bind);

        serviceCollection.AddRateLimiter(optionsAction);

        return serviceCollection;
    }
}