using Atlassian.Bitbucket.Application.Workspaces.Commands.CreateWebhook;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Application.Workspaces.Commands;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddWorkspaceCommands(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddCreateWebhookCommand();
        
        return serviceCollection;
    }
}