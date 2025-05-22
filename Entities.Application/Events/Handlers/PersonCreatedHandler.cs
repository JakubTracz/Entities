using Entities.Domain.People.Domain.Events;
using Entities.Domain.People.Repositories;
using Entities.SharedKernel.Events.Handlers;

namespace Entities.Application.Events.Handlers;

public class PersonCreatedHandler : IDomainEventHandler<PersonCreated>
{
    private readonly IPersonRepository _personRepository;

    public PersonCreatedHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    public async Task HandleAsync(PersonCreated e, CancellationToken cancellationToken = default)
    {
        var person = await _personRepository.GetById(e.PersonId);
        
        if (person is null)
        {
            Console.WriteLine($"Person with ID {e.PersonId.Id} not found.");
            return;
        }
        
        Console.WriteLine($"{person.Name.Value} {person.LastName.Value} added to the system on {e.OccurredOn} via an event with ID {e.Id}.");
    }
}