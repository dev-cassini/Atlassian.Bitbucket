using System.Text;
using MediatR;
using Newtonsoft.Json;

namespace Atlassian.Bitbucket.Application.Workspaces.Commands.CreateWebhook;

public class CommandHandler(HttpClient httpClient) : IRequestHandler<Command, Unit>
{
    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        var content = JsonConvert.SerializeObject(request);
        
        var response = await httpClient.PostAsync(
            new Uri("workspaces/dev-falc/hooks"),
            new StringContent(content, Encoding.UTF8, "application/json"),
            cancellationToken);

        return new Unit();
    }
}