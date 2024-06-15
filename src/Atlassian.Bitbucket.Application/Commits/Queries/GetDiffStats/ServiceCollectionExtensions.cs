using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddGetCommitDiffStatQuery(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IRequestHandler<Query, Response>, QueryHandler>((serviceProvider, httpClient) =>
            {
                var bitbucket = serviceProvider.GetRequiredService<IOptions<Configuration>>().Value;
                httpClient.BaseAddress = new Uri(bitbucket.ApiBaseUrl);
            })
            .AddHttpMessageHandler<Auth.Http.Handler>();
        
        return serviceCollection;
    }
}