using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class LocationTypeValidator : AbstractValidator<Entities.LocationType>
    {
        public LocationTypeValidator()
        {
            RuleFor(locationType => locationType.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(locationType => locationType.Name).NotNull().NotEmpty().MaximumLength(Constants.Constants.StringMaxLength);
        }
    }
}
