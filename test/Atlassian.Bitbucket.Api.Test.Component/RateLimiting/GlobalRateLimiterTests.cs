using System.Net;

namespace Atlassian.Bitbucket.Api.Test.Component.RateLimiting;

/// <summary>
/// Test global rate limiter using health endpoint (chosen for it's simplicity, but rate limiter
/// should be global and apply to all endpoints).
/// </summary>
[TestFixture]
public class GlobalRateLimiterTests
{
    /// <summary>
    /// Dispose and rebuild web application factory so that rate limiter counts are reset.
    /// </summary>
    [SetUp]
    public async Task SetUpAsync()
    {
        await OneTimeSetUpFixture.DisposeAndRebuildAsync();
    }
    
    [TearDown]
    public void TearDown()
    {
        OneTimeSetUpFixture.Configuration["RateLimiting:ConcurrentPermitLimit"] = "999";
        OneTimeSetUpFixture.Configuration["RateLimiting:ConcurrentQueueLimit"] = "999";
        OneTimeSetUpFixture.Configuration["RateLimiting:FixedWindowPermitLimit"] = "999";
        OneTimeSetUpFixture.Configuration["RateLimiting:FixedWindowQueueLimit"] = "999";
    }
    
    [Test]
    public async Task WhenNumberOfConcurrentRequestsExceedsPermitLimit_Then429TooManyRequestsIsReturned()
    {
        // Arrange
        OneTimeSetUpFixture.Configuration["RateLimiting:ConcurrentPermitLimit"] = "1";
        OneTimeSetUpFixture.Configuration["RateLimiting:ConcurrentQueueLimit"] = "0";
        
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
    
    [Test]
    public async Task WhenNumberOfRequestsExceedsFixedWindowPermitLimit_Then429TooManyRequestsIsReturned()
    {
        // Arrange
        OneTimeSetUpFixture.Configuration["RateLimiting:FixedWindowPermitLimit"] = "1";
        OneTimeSetUpFixture.Configuration["RateLimiting:FixedWindowQueueLimit"] = "0";
        
        var httpClient = OneTimeSetUpFixture.HttpClient;

        // Act
        var response1 = await httpClient.GetAsync("/health");
        var response2 = await httpClient.GetAsync("/health");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response1.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response2.StatusCode, Is.EqualTo(HttpStatusCode.TooManyRequests));
        });
    }
}