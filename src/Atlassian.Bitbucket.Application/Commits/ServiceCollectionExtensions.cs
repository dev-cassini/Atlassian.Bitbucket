using Atlassian.Bitbucket.Application.Commits.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Application.Commits;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddCommitServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddCommitQueries();
        
        return serviceCollection;
    }
}