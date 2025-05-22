using Entities.SharedKernel;

namespace Entities.Domain.Meetings.Domain.ValueObjects;

public class Place
{
    private readonly string _value;

    private Place(string value)
    {
        Value = value;
    }

    public static Place Create(string value)
    {
        return new Place(value);
    }

    public string Value
    {
        get => _value;
        private init
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Place cannot be null or empty.", nameof(value));
            }
            _value = value;
        }
    }

    protected bool Equals(Place other)
    {
        return _value == other._value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Place)obj);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}