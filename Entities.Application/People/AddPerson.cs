namespace Entities.Application.People;

public record AddPerson(
    string Name,
    string LastName,
    string City,
    string PostCode,
    string Street,
    string HouseNumber,
    int Age);