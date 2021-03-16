using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class AddressValidator : AbstractValidator<Entities.Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(address => address.City).NotNull().NotEmpty().MaximumLength(Constants.STRING_MAX_LENGTH);
            RuleFor(address => address.Street).NotNull().NotEmpty().MaximumLength(Constants.STRING_MAX_LENGTH);
            RuleFor(address => address.Latitude).NotNull();
            RuleFor(address => address.Longitude).NotNull();
            RuleFor(address => address.Location).SetValidator(new LocationValidator());
        }
    }
}
