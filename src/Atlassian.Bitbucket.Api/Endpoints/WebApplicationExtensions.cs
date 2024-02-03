using Atlassian.Bitbucket.Api.Endpoints.Webhooks;
using Atlassian.Bitbucket.Api.Endpoints.Workspaces;

namespace Atlassian.Bitbucket.Api.Endpoints;

public static class WebApplicationExtensions
{
    public static WebApplication RegisterEndpoints(this WebApplication webApplication)
    {
        webApplication
            .RegisterWebhookEndpoints()
            .RegisterWorkspaceEndpoints();

        return webApplication;
    }
}