using Atlassian.Bitbucket.Application.Workspaces.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Application.Workspaces;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddWorkspaceServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddWorkspaceCommands();
        
        return serviceCollection;
    }
}