using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Atlassian.Bitbucket.Api.Test.Component;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(serviceCollection =>
            {
                serviceCollection.AddMassTransitTestHarness();
            });
    }
}