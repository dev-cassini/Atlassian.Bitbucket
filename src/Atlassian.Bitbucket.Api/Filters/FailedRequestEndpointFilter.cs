using Atlassian.Bitbucket.Domain.Model;

namespace Atlassian.Bitbucket.Api.Filters;

public class FailedRequestEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next.Invoke(context);
        var statusCodeHttpResult = result as IStatusCodeHttpResult;

        if (statusCodeHttpResult?.StatusCode is < 200 && statusCodeHttpResult.StatusCode is > 299)
        {
            var failedRequest = new FailedRequest(
                Guid.NewGuid(),
                context.HttpContext,
                (result as IStatusCodeHttpResult)?.StatusCode ?? StatusCodes.Status418ImATeapot);
        }
        
        return result;
    }
}