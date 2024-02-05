using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddGetCommitDiffStatQuery(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IRequestHandler<Query, Response>, QueryHandler>(client =>
            {
                client.BaseAddress = new Uri("https://api.bitbucket.org/2.0/");
            })
            .AddHttpMessageHandler<Auth.Http.Handler>();
        
        return serviceCollection;
    }
}