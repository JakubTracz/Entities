using System.Linq.Expressions;

namespace Entities.SharedKernel.Filtering;

public sealed class SimpleEfCoreFilteringStrategy<T> : EfCoreFilteringStrategy<T>
{
    public SimpleEfCoreFilteringStrategy(List<Expression<Func<T, bool>>> expressions)
    {
        Expressions = expressions;
    }
}