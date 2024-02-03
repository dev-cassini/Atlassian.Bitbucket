using Atlassian.Bitbucket.Api.Endpoints.Webhooks.PullRequests.Updated;

namespace Atlassian.Bitbucket.Api.Endpoints.Webhooks.PullRequests;

public static class WebApplicationExtensions
{
    public static WebApplication RegisterWebhookPullRequestEndpoints(this WebApplication webApplication)
    {
        webApplication
            .RegisterPullRequestUpdatedWebhookEndpoint();

        return webApplication;
    }
}