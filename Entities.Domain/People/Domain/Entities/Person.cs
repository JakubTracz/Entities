using Entities.Domain.People.Domain.Events;
using Entities.Domain.People.Domain.ValueObjects;
using Entities.SharedKernel.Attributes;
using Entities.SharedKernel.SharedKernel;

namespace Entities.Domain.People.Domain.Entities;

[FilterableEntity]
public partial class Person : BaseEntity<PersonId>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Person()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

    private Person(Name name, Address address, Age age, Name lastName)
    {
        Address = address;
        Name = name;
        Age = age;
        LastName = lastName;
        Id = new PersonId();
        AddDomainEvent(new PersonCreated(Guid.CreateVersion7(), Id));
    }
    
    public Name Name { get; set; }
    public Age Age { get; set; }
    public Address Address { get; set; }
    public Name LastName { get; set; }

    public static Person Create(Name name, Address address, Age age, Name lastName)
    {
        return new Person(name, address, age, lastName);
    }
}