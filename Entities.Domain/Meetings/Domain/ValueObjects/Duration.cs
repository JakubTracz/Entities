namespace Entities.Domain.Meetings.Domain.ValueObjects;

public class Duration
{
    private readonly decimal _value;

    public Duration()
    {
        
    }
    
    public Duration(decimal duration)
    {
        Value = duration;
    }

    public decimal Value
    {
        get => _value;
        private init
        {
            if (value <= 0)
            {
                throw new ArgumentException("Duration must be greater than zero.", nameof(value));
            }
            _value = value;
        }
    }
    
    protected bool Equals(Duration other)
    {
        return _value == other._value;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Duration)obj);
    }
    
    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}