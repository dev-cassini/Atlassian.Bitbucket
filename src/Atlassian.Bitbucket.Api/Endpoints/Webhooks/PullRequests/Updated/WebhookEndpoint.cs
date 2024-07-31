using Atlassian.Bitbucket.Api.Filters;
using MediatR;

namespace Atlassian.Bitbucket.Api.Endpoints.Webhooks.PullRequests.Updated;

using PullRequests = Application.Webhooks.PullRequests;

public static class WebhookEndpoint
{
    public static WebApplication RegisterPullRequestUpdatedWebhookEndpoint(this WebApplication webApplication)
    {
        webApplication.MapPost("/webhooks/pull-requests/updated", Handler)
            .Accepts<PullRequests.Updated.Request>("application/json")
            .AllowAnonymous()
            .AddEndpointFilter<FailedRequestEndpointFilter>()
            .WithTags(nameof(Workspaces))
            .Produces(StatusCodes.Status204NoContent);

        return webApplication;
    }
    
    /// <summary>
    /// Process the updated pull request.
    /// </summary>
    /// <param name="request">Request.</param>
    /// <param name="mediator">Mediator.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    private static async Task<IResult> Handler(
        PullRequests.Updated.Request request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
        return Results.NoContent();
    }
}