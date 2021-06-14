using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class AddressValidator : AbstractValidator<Entities.Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(address => address.Name).NotNull().NotEmpty();
            RuleFor(address => address.Latitude).NotNull();
            RuleFor(address => address.Longitude).NotNull();
            RuleFor(address => address.Location).SetValidator(new LocationValidator()!);
        }
    }
}
