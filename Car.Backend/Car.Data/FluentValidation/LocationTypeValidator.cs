using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class LocationTypeValidator : AbstractValidator<Entities.LocationType>
    {
        public LocationTypeValidator()
        {
            RuleFor(locationType => locationType.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(locationType => locationType.Name).NotNull().NotEmpty().MaximumLength(Constants.STRING_MAX_LENGTH);
        }
    }
}
