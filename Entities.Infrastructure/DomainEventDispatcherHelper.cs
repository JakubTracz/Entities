using Entities.Infrastructure.Database;
using Entities.SharedKernel.Events;
using Entities.SharedKernel.SharedKernel;

namespace Entities.Infrastructure;

public static class DomainEventDispatcherHelper
{
    public static async Task DispatchDomainEventsAsync(
        AppDbContext dbContext,
        IDomainEventQueue queue,
        CancellationToken cancellationToken = default)
    {
        var aggregates = dbContext
            .ChangeTracker
            .Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Count != 0)
            .Select(e => e.Entity)
            .ToList();

        foreach (var aggregate in aggregates)
        {
            foreach (var domainEvent in aggregate.DomainEvents)
            {
                await queue.EnqueueAsync(domainEvent, cancellationToken);
            }

            aggregate.ClearDomainEvents();
        }
    }
}
