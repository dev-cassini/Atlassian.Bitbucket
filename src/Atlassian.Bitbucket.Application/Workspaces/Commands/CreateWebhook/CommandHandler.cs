using System.Text;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Atlassian.Bitbucket.Application.Workspaces.Commands.CreateWebhook;

public class CommandHandler(HttpClient httpClient) : IRequestHandler<Command>
{
    public async Task Handle(Command request, CancellationToken cancellationToken)
    {
        var content = JsonConvert.SerializeObject(request, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        });
        
        var response = await httpClient.PostAsync(
            "workspaces/dev-falc/hooks",
            new StringContent(content, Encoding.UTF8, "application/json"),
            cancellationToken);
    }
}