using Entities.SharedKernel.Events.Base;

namespace Entities.SharedKernel.SharedKernel;

public abstract class AggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    public void ClearDomainEvents() => _domainEvents.Clear();
}
