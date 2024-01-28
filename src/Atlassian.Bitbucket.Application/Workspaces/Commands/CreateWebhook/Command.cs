using MediatR;

namespace Atlassian.Bitbucket.Application.Workspaces.Commands.CreateWebhook;

/// <summary>
/// Command to create an active workspace webhook.
/// </summary>
/// <param name="Description">Description of the webhook.</param>
/// <param name="Url">The URL events get delivered to.</param>
/// <param name="Events">The events the webhook is subscribed to.</param>
public record Command(
    string Description,
    string Url,
    IEnumerable<string> Events) : IRequest<Unit>;