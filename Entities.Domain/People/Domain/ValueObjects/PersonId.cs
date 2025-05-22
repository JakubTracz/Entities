namespace Entities.Domain.People.Domain.ValueObjects;

public class PersonId(Guid guid)
{
    public Guid Id { get; } = guid;

    public PersonId() : this(Guid.CreateVersion7())
    {
    }
}