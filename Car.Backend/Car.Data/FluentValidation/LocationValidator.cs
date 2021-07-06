using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class LocationValidator : AbstractValidator<Entities.Location>
    {
        public LocationValidator()
        {
            RuleFor(location => location.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(location => location.Name).NotNull().NotEmpty();

            RuleFor(location => location.TypeId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(location => location.AddressId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(location => location.UserId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(location => location.Type).SetValidator(new LocationTypeValidator()!);
            RuleFor(location => location.User).SetValidator(new UserValidator()!);
        }
    }
}
