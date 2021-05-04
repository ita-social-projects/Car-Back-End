using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class JourneyPointDtoValidator : AbstractValidator<Dto.JourneyPointDto>
    {
        public JourneyPointDtoValidator()
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
        }
    }
}