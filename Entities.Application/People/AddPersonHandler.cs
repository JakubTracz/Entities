using Entities.Domain.People.Domain.Entities;
using Entities.Domain.People.Domain.ValueObjects;
using Entities.Domain.People.Repositories;
using Entities.SharedKernel.Events.Events;
using Microsoft.Extensions.Logging;

namespace Entities.Application.People;

public class AddPersonHandler
{
    private readonly ILogger<AddPersonHandler> _logger;
    private readonly IPersonRepository _personRepository;

    public AddPersonHandler(ILogger<AddPersonHandler> logger, IPersonRepository personRepository)
    {
        _logger = logger;
        _personRepository = personRepository;
    }

    public async Task<PersonAdded> Handle(AddPerson addPerson)
    {
        var person = Person.Create(
            new Name(addPerson.Name),
            Address.Create(addPerson.City, addPerson.PostCode, addPerson.Street, addPerson.HouseNumber),
            new Age(addPerson.Age), new Name(addPerson.LastName));

        var createdPerson = await _personRepository.Create(person);

        _logger.LogInformation("Person added: {Name}", createdPerson.Name);
        return new PersonAdded(createdPerson.Id.Id, createdPerson.Name.Value);
    }
}