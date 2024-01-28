using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Application.Workspaces.Commands.CreateWebhook;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddCreateWebhookCommand(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IRequestHandler<Command, Unit>, CommandHandler>(client =>
        {
            client.BaseAddress = new Uri("https://api.bitbucket.org/2.0/");
        });
        return serviceCollection;
    }
}