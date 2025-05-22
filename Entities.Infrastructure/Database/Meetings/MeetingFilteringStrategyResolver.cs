using System.Linq.Expressions;
using Entities.Domain.Meetings.Domain.Entities;
using Entities.SharedKernel.Filtering.Resolvers;

namespace Entities.Infrastructure.Database.Meetings;

public sealed class
    MeetingFilteringStrategyResolver : EfCoreFilteringStrategyResolver<Meeting>
{
    protected override void ApplyPredicates(string propertyName, string value, string filterOperator,
        List<Expression<Func<Meeting, bool>>> predicates)
    {
        Meeting.AppendNewStringPredicate(propertyName, value, filterOperator, predicates);
    }
}