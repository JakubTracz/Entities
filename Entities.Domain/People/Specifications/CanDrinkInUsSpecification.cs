using Ardalis.Specification;
using Entities.Domain.People.Domain.Entities;

namespace Entities.Domain.People.Specifications;

public class CanDrinkInUsSpecification : Specification<Person>
{
    public CanDrinkInUsSpecification()
    {
        Query
            .Where(p => p.Age.Value >= 21);
    }
}