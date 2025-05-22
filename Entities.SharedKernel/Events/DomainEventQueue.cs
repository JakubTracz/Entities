using System.Threading.Channels;
using Entities.SharedKernel.Events.Base;

namespace Entities.SharedKernel.Events;

public class DomainEventQueue : IDomainEventQueue
{
    private readonly Channel<IDomainEvent> _channel = Channel.CreateUnbounded<IDomainEvent>();
    
    public ValueTask EnqueueAsync(IDomainEvent domainEvent, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        return _channel.Writer.WriteAsync(domainEvent, token);
    }

    public ChannelReader<IDomainEvent> Reader => _channel.Reader;
}