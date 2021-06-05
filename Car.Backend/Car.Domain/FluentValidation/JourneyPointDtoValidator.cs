using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class JourneyPointDtoValidator : AbstractValidator<Dto.JourneyPointDto>
    {
        public JourneyPointDtoValidator()
        {
            RuleFor(point => point.Index).GreaterThanOrEqualTo(Constants.NumberMin);

            RuleFor(point => point.Latitude).InclusiveBetween(Constants.MinLatitude, Constants.MaxLatitude);

            RuleFor(point => point.Longitude).InclusiveBetween(Constants.MinLongitude, Constants.MaxLongitude);
        }
    }
}