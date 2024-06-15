using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Api.Test.Component;

[SetUpFixture]
public static class OneTimeSetUpFixture
{
    private static CustomWebApplicationFactory CustomWebApplicationFactory { get; set; } = null!;

    public static HttpClient HttpClient => CustomWebApplicationFactory.CreateClient();
    public static ITestHarness TestHarness { get; private set; } = null!;
    
    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        CustomWebApplicationFactory = new CustomWebApplicationFactory();
        TestHarness = CustomWebApplicationFactory.Services.GetRequiredService<ITestHarness>();
    }
}