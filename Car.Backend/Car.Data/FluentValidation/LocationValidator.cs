using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class LocationValidator : AbstractValidator<Entities.Location>
    {
        public LocationValidator()
        {
            RuleFor(location => location.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(location => location.Name).NotNull().NotEmpty().MaximumLength(Constants.STRING_MAX_LENGTH);
            RuleFor(location => location.TypeId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(location => location.AddressId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(location => location.UserId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(location => location.Type).SetValidator(new LocationTypeValidator());
            RuleFor(location => location.User).SetValidator(new UserValidator());
        }
    }
}
