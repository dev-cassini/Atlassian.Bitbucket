using System.Threading.RateLimiting;
using Atlassian.Bitbucket.Api.RateLimiting.Policies;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace Atlassian.Bitbucket.Api.RateLimiting;

internal static class RateLimiterOptionsExtensions
{
    internal static void ConfigureGlobalRateLimiter(this RateLimiterOptions rateLimiterOptions)
    {
        var fixedWindowRateLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        {
            var optionsMonitor = context.RequestServices.GetRequiredService<IOptionsMonitor<Configuration>>();
            return CustomFixedWindowRateLimiter.CreatePartition(optionsMonitor.CurrentValue);
        });
        
        var concurrencyRateLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        {
            var optionsMonitor = context.RequestServices.GetRequiredService<IOptionsMonitor<Configuration>>();
            return CustomConcurrencyRateLimiter.CreatePartition(optionsMonitor.CurrentValue);
        });
        
        var chainedPartitionedRateLimiter = PartitionedRateLimiter.CreateChained([fixedWindowRateLimiter, concurrencyRateLimiter]);
        rateLimiterOptions.GlobalLimiter = chainedPartitionedRateLimiter;
        
        rateLimiterOptions.OnRejected = (context, cancellationToken) =>
        {
            if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            {
                context.HttpContext.Response.Headers.RetryAfter = ((int) retryAfter.TotalSeconds).ToString();
            }

            context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", cancellationToken);

            return new ValueTask();
        };
    }
}