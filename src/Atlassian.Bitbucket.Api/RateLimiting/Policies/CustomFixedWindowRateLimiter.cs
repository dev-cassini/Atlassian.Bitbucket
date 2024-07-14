using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace Atlassian.Bitbucket.Api.RateLimiting.Policies;

public class CustomFixedWindowRateLimiter : IRateLimiterPolicy<string>
{
    private readonly IOptionsMonitor<Configuration> _configurationSnapshot;
    
    public CustomFixedWindowRateLimiter(IOptionsMonitor<Configuration> optionsSnapshot)
    {
        OnRejected = (ctx, _) =>
        {
            ctx.HttpContext.Response.StatusCode = StatusCodes.Status418ImATeapot;
            return ValueTask.CompletedTask;
        };
        
        _configurationSnapshot = optionsSnapshot;
    }

    public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected { get; }

    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        return CreatePartition(_configurationSnapshot.CurrentValue);
    }

    public static RateLimitPartition<string> CreatePartition(Configuration configuration)
    {
        return RateLimitPartition.GetFixedWindowLimiter(string.Empty,
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = configuration.FixedWindowPermitLimit,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = configuration.FixedWindowQueueLimit,
                Window = new TimeSpan(hours: 0, minutes: 1, seconds: 0)
            });
    }
}