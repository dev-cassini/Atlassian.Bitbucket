using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace Atlassian.Bitbucket.Api.RateLimiting.Policies;

public class CustomSlidingWindowRateLimiter : IRateLimiterPolicy<string>
{
    private readonly IOptionsMonitor<Configuration> _configurationSnapshot;

    public CustomSlidingWindowRateLimiter(IOptionsMonitor<Configuration> optionsSnapshot)
    {
        OnRejected = (ctx, token) =>
        {
            ctx.HttpContext.Response.StatusCode = StatusCodes.Status418ImATeapot;
            return ValueTask.CompletedTask;
        };
        
        _configurationSnapshot = optionsSnapshot;
    }

    public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected { get; }

    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        return RateLimitPartition.GetSlidingWindowLimiter(string.Empty,
            _ => new SlidingWindowRateLimiterOptions
            {
                PermitLimit = _configurationSnapshot.CurrentValue.PermitLimit,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = _configurationSnapshot.CurrentValue.QueueLimit,
                Window = new TimeSpan(hours: 0, minutes: 1, seconds: 0),
                SegmentsPerWindow = 6
            });;
    }
}