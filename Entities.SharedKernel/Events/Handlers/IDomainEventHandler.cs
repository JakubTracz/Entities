using Entities.SharedKernel.Events.Base;

namespace Entities.SharedKernel.Events.Handlers;

public interface IDomainEventHandler<in T> where T : IDomainEvent
{
    Task HandleAsync(T domainEvent, CancellationToken cancellationToken = default);
}
