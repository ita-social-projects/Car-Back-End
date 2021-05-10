using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class JourneyPointValidator : AbstractValidator<Entities.JourneyPoint>
    {
        public JourneyPointValidator()
        {
            RuleFor(point => point.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(point => point.Index).GreaterThanOrEqualTo(Constants.Constants.NumberMin);

            RuleFor(point => point.Latitude)
                .GreaterThanOrEqualTo(Constants.Constants.MinLatitude)
                .LessThanOrEqualTo(Constants.Constants.MaxLatitude);
            RuleFor(point => point.Longitude)
                .GreaterThanOrEqualTo(Constants.Constants.MinLongitude)
                .LessThanOrEqualTo(Constants.Constants.MaxLongitude);

            RuleFor(point => point.JourneyId).GreaterThan(Constants.Constants.NumberMin);
            RuleFor(point => point.Journey).SetValidator(new JourneyValidator());
        }
    }
}