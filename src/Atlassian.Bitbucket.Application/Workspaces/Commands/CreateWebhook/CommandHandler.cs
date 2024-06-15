using System.Text;
using System.Text.Json;
using MediatR;

namespace Atlassian.Bitbucket.Application.Workspaces.Commands.CreateWebhook;

public class CommandHandler(HttpClient httpClient) : IRequestHandler<Command>
{
    public async Task Handle(Command request, CancellationToken cancellationToken)
    {
        var jsonSerializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
        var content = JsonSerializer.Serialize(request, jsonSerializeOptions);
        
        await httpClient.PostAsync(
            "workspaces/dev-falc/hooks",
            new StringContent(content, Encoding.UTF8, "application/json"),
            cancellationToken);
    }
}