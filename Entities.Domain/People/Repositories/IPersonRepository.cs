using Ardalis.Specification;
using Entities.Domain.People.Domain.ValueObjects;
using Person = Entities.Domain.People.Domain.Entities.Person;

namespace Entities.Domain.People.Repositories;

public interface IPersonRepository
{
    Task<List<Person>> GetAll(ISpecification<Person> specification);
    Task<List<Person>> GetAll(string? filterBy);
    Task<Person> Create(Person person);
    Task<Person?> GetById(PersonId id);
}