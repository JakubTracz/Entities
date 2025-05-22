using Entities.Application.People;
using FluentValidation;

namespace Entities.Api.Validators;

public class AddPersonValidator : AbstractValidator<AddPerson>
{
    public AddPersonValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required.");

        RuleFor(x => x.PostCode)
            .NotEmpty()
            .WithMessage("Post code is required.");

        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Street is required.");

        RuleFor(x => x.HouseNumber)
            .NotEmpty()
            .WithMessage("House number is required.");

        RuleFor(x => x.Age)
            .GreaterThan(0)
            .WithMessage("Age must be greater than 0.");
    }
}