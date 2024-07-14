using System.Net;

namespace Atlassian.Bitbucket.Api.Test.Component.RateLimiting;

/// <summary>
/// Test global rate limiter using health endpoint (chosen for it's simplicity, but rate limiter
/// should be global and apply to all endpoints).
/// </summary>
[TestFixture]
public class GlobalRateLimiterTests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        OneTimeSetUpFixture.Configuration["RateLimiting:ConcurrentPermitLimit"] = "1";
        OneTimeSetUpFixture.Configuration["RateLimiting:ConcurrentQueueLimit"] = "0";
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        OneTimeSetUpFixture.Configuration["RateLimiting:ConcurrentPermitLimit"] = "999";
        OneTimeSetUpFixture.Configuration["RateLimiting:ConcurrentQueueLimit"] = "999";
    }
    
    [Test]
    public async Task WhenConcurrentRequestsAreSubmitted_ThenOnlyOneIsProcessed()
    {
        // Arrange
        var httpClient = OneTimeSetUpFixture.HttpClient;
        
        var task1 = Task.Run(async () => await httpClient.GetAsync("/health"));
        var task2 = Task.Run(async () => await httpClient.GetAsync("/health"));

        // Act
        await Task.WhenAll([task1, task2]);

        // Assert
        var results = new List<HttpResponseMessage> { task1.Result, task2.Result };
        Assert.Multiple(() =>
        {
            Assert.That(results.Count(x => x.StatusCode is HttpStatusCode.OK), Is.EqualTo(1));
            Assert.That(results.Count(x => x.StatusCode is HttpStatusCode.TooManyRequests), Is.EqualTo(1));
        });
    }
}