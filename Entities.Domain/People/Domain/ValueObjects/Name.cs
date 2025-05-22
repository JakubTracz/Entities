using Entities.SharedKernel;

namespace Entities.Domain.People.Domain.ValueObjects;

public class Name
{
    private readonly string _value;

    public Name(string value)
    {
        Value = value;
    }

    public string Value
    {
        get => _value;
        private init
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(value));
            }
            
            _value = value;
        }
    }

    protected bool Equals(Name other)
    {
        return _value == other._value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Name)obj);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}