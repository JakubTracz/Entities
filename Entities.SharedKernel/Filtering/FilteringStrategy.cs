using System.Linq.Expressions;

namespace Entities.SharedKernel.Filtering;

public abstract class FilteringStrategy<T>
{
    public List<Expression<Func<T, bool>>> Expressions { get; init; }
}