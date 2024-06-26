using Atlassian.Bitbucket.Application.Auth;
using Atlassian.Bitbucket.Application.Commits;
using Atlassian.Bitbucket.Application.Workspaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddOptions<Configuration>()
            .Configure(configuration.GetSection(nameof(Bitbucket)).Bind);
        
        serviceCollection
            .AddAuthServices(configuration)
            .AddCommitServices()
            .AddWorkspaceServices();
        
        return serviceCollection;
    }
}