using Atlassian.Bitbucket.Api.Endpoints.Webhooks.PullRequests;

namespace Atlassian.Bitbucket.Api.Endpoints.Webhooks;

public static class WebApplicationExtensions
{
    public static WebApplication RegisterWebhookEndpoints(this WebApplication webApplication)
    {
        webApplication
            .RegisterWebhookPullRequestEndpoints();

        return webApplication;
    }
}