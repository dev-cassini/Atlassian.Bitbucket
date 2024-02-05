using Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Application.Commits.Queries;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddCommitQueries(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddGetCommitDiffStatQuery();
        
        return serviceCollection;
    }
}