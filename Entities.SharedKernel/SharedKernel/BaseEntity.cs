namespace Entities.SharedKernel.SharedKernel;

public abstract class BaseEntity<T> : AggregateRoot
{
    public T Id { get; set; }
}