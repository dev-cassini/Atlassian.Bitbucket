using System.Net;
using System.Net.Http.Json;
using Atlassian.Bitbucket.Builders.Application.Commits.Queries.GetDiffStats;
using Atlassian.Bitbucket.Builders.Application.Webhooks.PullRequests.Updated;
using Atlassian.Bitbucket.Domain.Enums;
using Atlassian.Bitbucket.Integrations.Outbound.Notifications.PullRequests;
using MassTransit.Internals;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Atlassian.Bitbucket.Api.Test.Component.Endpoints.Webhooks.PullRequests.Updated;

[TestFixture]
public class WebhookEndpointTests
{
    [Test]
    public async Task ResponseIs204NoContent()
    {
        // Arrange
        var request = new RequestBuilder().State(PullRequestStates.Merged).Build();
        var commitDiffStatResponse = new ResponseBuilder().Build();

        OneTimeSetUpFixture.WireMockServer
            .Given(
                Request.Create()
                    .WithPath(
                        $"/repositories/dev-falc/{request.Repository.Uuid}/diffstat/{request.PullRequest.MergeCommit?.Hash}")
                    .UsingGet())
            .RespondWith(
                Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBodyAsJson(commitDiffStatResponse)
                );
        
        var httpClient = OneTimeSetUpFixture.HttpClient;

        // Act
        var response = await httpClient.PostAsJsonAsync("/webhooks/pull-requests/updated", request);
        
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
    
    [Test]
    public async Task WhenRequestIsForAMergePullRequest_ThenPullRequestMergedEventIsPublished()
    {
        // Arrange
        var request = new RequestBuilder().State(PullRequestStates.Merged).Build();
        var commitDiffStatResponse = new ResponseBuilder().Build();

        OneTimeSetUpFixture.WireMockServer
            .Given(
                Request.Create()
                    .WithPath(
                        $"/repositories/dev-falc/{request.Repository.Uuid}/diffstat/{request.PullRequest.MergeCommit?.Hash}")
                    .UsingGet())
            .RespondWith(
                Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBodyAsJson(commitDiffStatResponse)
            );
        
        var httpClient = OneTimeSetUpFixture.HttpClient;

        // Act
        await httpClient.PostAsJsonAsync("/webhooks/pull-requests/updated", request);
        
        // Assert
        var testHarness = OneTimeSetUpFixture.TestHarness;
        var pullRequestMergedEvents = await testHarness.Published.SelectAsync<PullRequestMerged>().ToListAsync();
        
        Assert.That(pullRequestMergedEvents, Has.Count.EqualTo(1));

        var pullRequestMergedEvent = pullRequestMergedEvents.Single().MessageObject as PullRequestMerged;
        Assert.That(pullRequestMergedEvent!.Id, Is.EqualTo(request.PullRequest.Id));
    }
}