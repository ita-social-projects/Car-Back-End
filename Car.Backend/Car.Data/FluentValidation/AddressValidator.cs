using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class AddressValidator : AbstractValidator<Entities.Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Id).GreaterThan(0);
            RuleFor(address => address.Street).NotNull().Length(2, 50);
            RuleFor(address => address.City).NotNull().Length(2, 50);
            RuleFor(address => address.Latitude).NotNull();
            RuleFor(address => address.Longitude).NotNull();
        }
    }
}
