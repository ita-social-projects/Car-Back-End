using System;
using Car.Data.Constants;
using Car.Domain.Filters;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class JourneyFilterValidator : AbstractValidator<JourneyFilter>
    {
        public JourneyFilterValidator()
        {
            RuleFor(filter => filter.DepartureTime).GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(filter => filter.ApplicantId).GreaterThan(Constants.IdLength);
            RuleFor(filter => filter.Fee).IsInEnum();
            RuleFor(filter => filter.PassengersCount).InclusiveBetween(Constants.SeatsMinCount, Constants.SeatsMaxCount);
            RuleFor(filter => filter.FromLatitude).InclusiveBetween(Constants.MinLatitude, Constants.MaxLatitude);
            RuleFor(filter => filter.FromLongitude).InclusiveBetween(Constants.MinLongitude, Constants.MaxLongitude);
            RuleFor(filter => filter.ToLatitude).InclusiveBetween(Constants.MinLatitude, Constants.MaxLatitude);
            RuleFor(filter => filter.ToLongitude).InclusiveBetween(Constants.MinLongitude, Constants.MaxLongitude);
        }
    }
}
