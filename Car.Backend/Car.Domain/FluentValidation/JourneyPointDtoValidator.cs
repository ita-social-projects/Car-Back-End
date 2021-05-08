using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class JourneyPointDtoValidator : AbstractValidator<Dto.JourneyPointDto>
    {
        public JourneyPointDtoValidator()
        {
            RuleFor(point => point.Id).GreaterThan(Constants.IdLength);
            RuleFor(point => point.Index).GreaterThanOrEqualTo(Constants.NumberMin);

            RuleFor(point => point.Latitude)
                .GreaterThanOrEqualTo(Constants.MinLatitude)
                .LessThanOrEqualTo(Constants.MaxLatitude);
            RuleFor(point => point.Longitude)
                .GreaterThanOrEqualTo(Constants.MinLongitude)
                .LessThanOrEqualTo(Constants.MaxLongitude);

            RuleFor(point => point.JourneyId).GreaterThan(Constants.NumberMin);
        }
    }
}