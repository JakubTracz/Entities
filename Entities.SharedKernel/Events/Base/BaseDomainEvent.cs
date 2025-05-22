namespace Entities.SharedKernel.Events.Base;

public abstract record BaseDomainEvent(Guid Id) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}