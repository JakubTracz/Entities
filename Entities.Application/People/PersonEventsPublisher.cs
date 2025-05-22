using Entities.Domain.People.Domain.Entities;
using Entities.Domain.People.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wolverine;

namespace Entities.Application.People;

public class PersonEventsPublisher : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PersonEventsPublisher(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var person = Person.Create(
                new Name("John"),
                Address.Create("123 Main St", "Springfield", "IL", "62704"),
                new Age(30),
                new Name("Doe")
            );
            // await messageBus.SendAsync(new AddPerson(person));
            await Task.Delay(1000, stoppingToken);
        }
    }
}