using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Atlassian.Bitbucket.Api.Test.Component;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseEnvironment("Development")
            .ConfigureAppConfiguration(configurationBuilder =>
            {
                var wireMockBaseUrl = OneTimeSetUpFixture.WireMockServer.Url;
                
                configurationBuilder
                    .AddInMemoryCollection(new List<KeyValuePair<string, string?>>
                    {
                        new("Bitbucket:OAuthBaseUrl", wireMockBaseUrl),
                        new("Bitbucket:ApiBaseUrl", wireMockBaseUrl),
                        new("Bitbucket:Client:Id", "CLIENT_ID"),
                        new("Bitbucket:Client:Secret", "CLIENT_SECRET"),
                        new("RateLimiting:FixedWindowPermitLimit", "999"),
                        new("RateLimiting:FixedWindowQueueLimit", "999"),
                        new("RateLimiting:ConcurrentPermitLimit", "999"),
                        new("RateLimiting:ConcurrentQueueLimit", "999")
                    });
            })
            .ConfigureServices(serviceCollection =>
            {
                serviceCollection.AddMassTransitTestHarness();
            });
    }
}