using MediatR;

namespace Atlassian.Bitbucket.Domain.Model;

public abstract class Entity
{
    private readonly List<INotification> _domainEvents = new();
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification @event)
    {
        _domainEvents.Remove(@event);
    }
}