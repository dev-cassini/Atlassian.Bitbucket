using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace Atlassian.Bitbucket.Api.RateLimiting.Policies;

public class CustomConcurrencyRateLimiter : IRateLimiterPolicy<string>
{
    private readonly IOptionsMonitor<Configuration> _configurationSnapshot;
    
    public CustomConcurrencyRateLimiter(IOptionsMonitor<Configuration> optionsSnapshot)
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
        return RateLimitPartition.GetConcurrencyLimiter(string.Empty,
            _ => new ConcurrencyLimiterOptions
            {
                PermitLimit = configuration.ConcurrentPermitLimit,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = configuration.ConcurrentQueueLimit
            });
    }
}