using System.Linq.Expressions;
using Entities.Domain.People.Domain.Entities;
using Entities.SharedKernel.Filtering.Resolvers;

namespace Entities.Infrastructure.Database.People;

public sealed class PersonFilteringStrategyResolver : EfCoreFilteringStrategyResolver<Person>
{
    protected override void ApplyPredicates(string propertyName, string value, string filterOperator,
        List<Expression<Func<Person, bool>>> predicates)
    {
        Person.AppendNewStringPredicate(propertyName, value, filterOperator, predicates);
    }
}