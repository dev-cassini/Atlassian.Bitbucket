using Microsoft.Extensions.DependencyInjection;

namespace Atlassian.Bitbucket.Infrastructure.Messaging.MediatR;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddMediatR(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(configuration =>
        {
            configuration
                .RegisterServicesFromAssemblyContaining<Application.Marker>()
                .RegisterServicesFromAssemblyContaining<Integrations.Outbound.Marker>();
        });
        
        return serviceCollection;
    }
}