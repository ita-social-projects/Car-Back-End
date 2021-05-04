using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class JourneyPointValidator : AbstractValidator<Entities.JourneyPoint>
    {
        public JourneyPointValidator()
        {
            RuleFor(point => point.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(point => point.Index).GreaterThanOrEqualTo(Constants.NUMBER_MIN);

            RuleFor(point => point.Latitude)
                .GreaterThanOrEqualTo(Constants.MIN_LATITUDE)
                .LessThanOrEqualTo(Constants.MAX_LATITUDE);
            RuleFor(point => point.Longitude)
                .GreaterThanOrEqualTo(Constants.MIN_LONGITUDE)
                .LessThanOrEqualTo(Constants.MAX_LONGITUDE);

            RuleFor(point => point.JourneyId).GreaterThan(Constants.NUMBER_MIN);
            RuleFor(point => point.Journey).SetValidator(new JourneyValidator());
        }
    }
}