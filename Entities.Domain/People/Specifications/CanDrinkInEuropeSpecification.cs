using Ardalis.Specification;
using Entities.Domain.People.Domain.Entities;

namespace Entities.Domain.People.Specifications;

public class CanDrinkInEuropeSpecification : Specification<Person>
{
    public CanDrinkInEuropeSpecification()
    {
        Query
            .Where(p => p.Age.Value >= 18);
    }
}