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
            .ConfigureAppConfiguration(configurationBuilder =>
            {
                var wireMockBaseUrl = OneTimeSetUpFixture.WireMockServer.Url;
                configurationBuilder
                    .AddInMemoryCollection(new List<KeyValuePair<string, string?>>
                    {
                        new("Bitbucket:OAuthBaseUrl", wireMockBaseUrl),
                        new("Bitbucket:ApiBaseUrl", wireMockBaseUrl)
                    });
            })
            .ConfigureServices(serviceCollection =>
            {
                serviceCollection.AddMassTransitTestHarness();
            });
    }
}