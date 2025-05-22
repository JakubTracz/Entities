using Entities.SharedKernel.Events.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Entities.SharedKernel.Events;

public class DomainEventDispatcher : BackgroundService
{
    private readonly IDomainEventQueue _queue;
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IDomainEventQueue queue, IServiceProvider serviceProvider)
    {
        _queue = queue;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var domainEvent in _queue.Reader.ReadAllAsync(stoppingToken))
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                var method = handlerType.GetMethod("HandleAsync")!;
                await (Task)method.Invoke(handler, [domainEvent, stoppingToken])!;
            }
        }
    }
}
