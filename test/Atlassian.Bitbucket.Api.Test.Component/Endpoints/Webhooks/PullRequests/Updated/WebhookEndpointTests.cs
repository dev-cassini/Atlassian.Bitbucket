using System.Net;
using System.Net.Http.Json;
using Atlassian.Bitbucket.Builders.Application.Webhooks.PullRequests.Updated;
using Atlassian.Bitbucket.Domain.Enums;

namespace Atlassian.Bitbucket.Api.Test.Component.Endpoints.Webhooks.PullRequests.Updated;

[TestFixture]
public class WebhookEndpointTests
{
    [Test]
    public async Task WhenRequestIsForAnOpenPullRequest_ThenResponseIs204NoContent()
    {
        var request = new RequestBuilder().State(PullRequestStates.Open).Build();
        
        var httpClient = OneTimeSetUpFixture.HttpClient;
        var response = await httpClient.PostAsJsonAsync("/webhooks/pull-requests/updated", request);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}