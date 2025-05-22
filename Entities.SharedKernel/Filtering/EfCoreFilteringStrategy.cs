using System.Linq.Expressions;

namespace Entities.SharedKernel.Filtering;

public abstract class EfCoreFilteringStrategy<T>
{
    public List<Expression<Func<T, bool>>> Expressions { get; init; } = [];
}