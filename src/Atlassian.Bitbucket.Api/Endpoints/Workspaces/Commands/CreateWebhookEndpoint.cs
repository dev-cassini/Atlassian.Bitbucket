using MediatR;

namespace Atlassian.Bitbucket.Api.Endpoints.Workspaces.Commands;

using CreateWebhook = Application.Workspaces.Commands.CreateWebhook;

public static class CreateWebhookEndpoint
{
    public static WebApplication RegisterCreateWebhookEndpoint(this WebApplication webApplication)
    {
        webApplication.MapPost("/workspaces/dev-falc/webhooks", CreateWebhook)
            .AllowAnonymous()
            .WithTags(nameof(Workspaces))
            .Produces(StatusCodes.Status204NoContent);

        return webApplication;
    }
    
    /// <summary>
    /// Creates an active workspace webhook.
    /// </summary>
    /// <param name="command">Command.</param>
    /// <param name="mediator">Mediator.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    private static async Task<IResult> CreateWebhook(
        CreateWebhook.Command command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return Results.NoContent();
    }
}