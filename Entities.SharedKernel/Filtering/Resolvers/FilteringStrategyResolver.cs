using System.Diagnostics.CodeAnalysis;

namespace Entities.SharedKernel.Filtering.Resolvers;

public abstract class FilteringStrategyResolver<T>
{
    protected bool NoFilterApplied([NotNullWhen(false)]string? filterBy)
    {
        return filterBy.IsNullOrWhiteSpace() ||
               (!filterBy!.Contains(FilteringConstants.Delimiter) && filterBy.EndsWith(" \'\'"));
    }

    protected string[] GetFilterPairs(string filterBy)
    {
        return filterBy.Split(FilteringConstants.Delimiter);
    }
    
    public abstract FilteringStrategy<T> ResolveFilteringStrategy(string? filterBy);
}