using Car.Data.Constants;
using Car.Domain.Models.Journey;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CreateJourneyPointModelValidator : AbstractValidator<CreateJourneyPointModel>
    {
        public CreateJourneyPointModelValidator()
        {
            RuleFor(point => point.Index).GreaterThanOrEqualTo(Constants.NumberMin);

            RuleFor(point => point.Latitude)
                .GreaterThanOrEqualTo(Constants.MinLatitude)
                .LessThanOrEqualTo(Constants.MaxLatitude);
            RuleFor(point => point.Longitude)
                .GreaterThanOrEqualTo(Constants.MinLongitude)
                .LessThanOrEqualTo(Constants.MaxLongitude);
        }
    }
}