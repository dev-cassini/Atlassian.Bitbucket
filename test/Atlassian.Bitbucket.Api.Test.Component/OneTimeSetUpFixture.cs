using System.Net;
using MassTransit.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Atlassian.Bitbucket.Api.Test.Component;

[SetUpFixture]
public static class OneTimeSetUpFixture
{
    private static CustomWebApplicationFactory CustomWebApplicationFactory { get; set; } = null!;

    public static HttpClient HttpClient => CustomWebApplicationFactory.CreateClient();
    public static ITestHarness TestHarness { get; private set; } = null!;
    public static WireMockServer WireMockServer { get; private set; } = null!;
    public static IConfiguration Configuration { get; private set; } = null!;
    
    [OneTimeSetUp]
    public static async Task OneTimeSetUpAsync()
    {
        WireMockServer = WireMockServer.Start();
        CustomWebApplicationFactory = new CustomWebApplicationFactory();
        TestHarness = CustomWebApplicationFactory.Services.GetRequiredService<ITestHarness>();
        Configuration = CustomWebApplicationFactory.Services.GetRequiredService<IConfiguration>();
        
        WireMockServer
            .Given(Request.Create().WithPath("/access_token")
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new
                {
                    access_token = "access_token",
                    expires_in = 3600
                }));
        
        await TestHarness.Start();
    }

    public static async Task DisposeAndRebuildAsync()
    {
        await TestHarness.Stop();
        await CustomWebApplicationFactory.DisposeAsync();
        CustomWebApplicationFactory = new CustomWebApplicationFactory();
        TestHarness = CustomWebApplicationFactory.Services.GetRequiredService<ITestHarness>();
        Configuration = CustomWebApplicationFactory.Services.GetRequiredService<IConfiguration>();
        await TestHarness.Start();
    }
}