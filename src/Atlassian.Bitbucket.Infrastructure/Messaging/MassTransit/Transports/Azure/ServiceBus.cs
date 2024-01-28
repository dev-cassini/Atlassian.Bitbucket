namespace Atlassian.Bitbucket.Infrastructure.Messaging.MassTransit.Transports.Azure;

public class ServiceBus
{
    public required string ConnectionString { get; init; }
}