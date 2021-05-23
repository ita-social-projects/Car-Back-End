using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class RequestValidator : AbstractValidator<Entities.Request>
    {
        public RequestValidator()
        {
            RuleFor(request => request.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(request => request.DepartureTime).GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(request => request.PassengersCount).GreaterThan(Constants.Constants.NumberMin);
            RuleFor(request => request.UserId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(request => request.User).NotNull().SetValidator(new UserValidator());
            RuleFor(request => request.Fee).IsInEnum();

            RuleFor(request => request.From.Latitude)
                .GreaterThanOrEqualTo(Constants.Constants.MinLatitude)
                .LessThanOrEqualTo(Constants.Constants.MaxLatitude);
            RuleFor(request => request.From.Longitude)
                .GreaterThanOrEqualTo(Constants.Constants.MinLongitude)
                .LessThanOrEqualTo(Constants.Constants.MaxLongitude);

            RuleFor(request => request.To.Latitude)
                .GreaterThanOrEqualTo(Constants.Constants.MinLatitude)
                .LessThanOrEqualTo(Constants.Constants.MaxLatitude);
            RuleFor(request => request.To.Longitude)
                .GreaterThanOrEqualTo(Constants.Constants.MinLongitude)
                .LessThanOrEqualTo(Constants.Constants.MaxLongitude);
        }
    }
}
