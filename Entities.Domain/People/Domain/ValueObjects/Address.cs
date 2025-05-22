namespace Entities.Domain.People.Domain.ValueObjects;

public class Address
{
    private readonly string _city;

    public string City
    {
        get => _city;
        private init
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("City cannot be null or empty.", nameof(value));
            }
            _city = value;
        }
    }

    public string PostCode { get; }

    public string Street { get; }

    public string HouseNumber { get; }

    private Address(string city, string postCode, string street, string houseNumber)
    {
        City = city;
        PostCode = postCode;
        Street = street;
        HouseNumber = houseNumber;
    }

    public static Address Create(string city, string postCode, string street, string houseNumber)
    {
        return new Address(city, postCode, street, houseNumber);
    }

    protected bool Equals(Address other)
    {
        return City == other.City && PostCode == other.PostCode && Street == other.Street &&
               HouseNumber == other.HouseNumber;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Address)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(City, PostCode, Street, HouseNumber);
    }
}