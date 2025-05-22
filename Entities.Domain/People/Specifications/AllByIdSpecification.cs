using Ardalis.Specification;
using Entities.Domain.People.Domain.Entities;
using Entities.Domain.People.Domain.ValueObjects;

namespace Entities.Domain.People.Specifications;

public class AllByIdSpecification : Specification<Person>
{
    public AllByIdSpecification(ICollection<Guid> ids)
    {
        var personIds = ids.Select(x => new PersonId(x)).ToList();
        Query
            .Where(x => personIds.Contains(x.Id));
    }
}