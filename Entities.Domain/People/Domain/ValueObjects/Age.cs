namespace Entities.Domain.People.Domain.ValueObjects;

public class Age
{
    public Age(int value)
    {
        Value = value;
    }

    public int Value { get; }
}