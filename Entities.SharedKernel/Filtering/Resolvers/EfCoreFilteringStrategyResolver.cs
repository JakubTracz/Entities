using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Entities.SharedKernel.SharedKernel;

namespace Entities.SharedKernel.Filtering.Resolvers;

public abstract class EfCoreFilteringStrategyResolver<T>
{
    private static bool NoFilterApplied([NotNullWhen(false)] string? filterBy)
    {
        return filterBy.IsNullOrWhiteSpace() ||
               (!filterBy!.Contains(FilteringConstants.Delimiter) &&
                filterBy.EndsWith(" \'\'"));
    }

    private static IEnumerable<string> GetFilterPairs(string filterBy)
    {
        return filterBy.Split(FilteringConstants.Delimiter);
    }

    public SimpleEfCoreFilteringStrategy<T>
        ResolveEfCoreFilteringStrategy(string? filterBy)
    {
        if (NoFilterApplied(filterBy))
        {
            return new SimpleEfCoreFilteringStrategy<T>([]);
        }

        var predicates = new List<Expression<Func<T, bool>>>();
        var filterPairs = GetFilterPairs(filterBy);

        foreach (var filter in filterPairs)
        {
            var filterParameters = new FilterParameters(filter);

            if (filterParameters.HaveNoFilter())
            {
                continue;
            }

            var value = filterParameters.Value;
            var propertyName = filterParameters.PropertyName;
            var filterOperator = filterParameters.FilterOperator;

            ApplyPredicates(propertyName, value, filterOperator, predicates);
        }

        return new SimpleEfCoreFilteringStrategy<T>(predicates);
    }

    protected virtual void ApplyPredicates(string propertyName, string value,
        string filterOperator, List<Expression<Func<T, bool>>> predicates)
    {
           
    }
}