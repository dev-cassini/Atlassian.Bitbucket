using Atlassian.Bitbucket.Api.Endpoints.Workspaces.Commands;

namespace Atlassian.Bitbucket.Api.Endpoints.Workspaces;

public static class WebApplicationExtensions
{
    public static WebApplication RegisterWorkspaceEndpoints(this WebApplication webApplication)
    {
        webApplication
            .RegisterCreateWebhookEndpoint();

        return webApplication;
    }
}