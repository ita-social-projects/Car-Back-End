using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class PointValidator : AbstractValidator<Entities.Point>
    {
        public PointValidator()
        {
            RuleFor(point => point.Latitude)
                .GreaterThanOrEqualTo(Constants.Constants.MinLatitude)
                .LessThanOrEqualTo(Constants.Constants.MaxLatitude);

            RuleFor(point => point.Longitude)
                .GreaterThanOrEqualTo(Constants.Constants.MinLongitude)
                .LessThanOrEqualTo(Constants.Constants.MaxLongitude);
        }
    }
}
