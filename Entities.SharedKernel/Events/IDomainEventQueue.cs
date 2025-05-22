using System.Threading.Channels;
using Entities.SharedKernel.Events.Base;

namespace Entities.SharedKernel.Events;

public interface IDomainEventQueue
{
    ValueTask EnqueueAsync(IDomainEvent domainEvent, CancellationToken token = default);
    ChannelReader<IDomainEvent> Reader { get; }
}