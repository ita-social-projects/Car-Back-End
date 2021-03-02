using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class LocationValidator : AbstractValidator<Entities.Location>
    {
        public LocationValidator()
        {
            RuleFor(location => location.Id).GreaterThan(Constants.IDLENGTH);
            RuleFor(location => location.Name).NotNull().NotEmpty().MaximumLength(Constants.STRINGMAXLENGTH);
            RuleFor(location => location.TypeId).GreaterThan(Constants.IDLENGTH);
            RuleFor(location => location.AddressId).GreaterThan(Constants.IDLENGTH);
            RuleFor(location => location.UserId).GreaterThan(Constants.IDLENGTH);
            RuleFor(location => location.Type).SetValidator(new LocationTypeValidator());
            RuleFor(location => location.User).SetValidator(new UserValidator());
        }
    }
}
