using System;
using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class RequestDtoValidator : AbstractValidator<RequestDto>
    {
        public RequestDtoValidator()
        {
            RuleFor(request => request.DepartureTime).GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(request => request.PassengersCount).GreaterThan(Constants.NumberMin);
            RuleFor(request => request.UserId).GreaterThan(Constants.IdLength);
            RuleFor(request => request.Fee).IsInEnum();

            RuleFor(request => request.From.Latitude)
                .GreaterThanOrEqualTo(Constants.MinLatitude)
                .LessThanOrEqualTo(Constants.MaxLatitude);
            RuleFor(request => request.From.Longitude)
                .GreaterThanOrEqualTo(Constants.MinLongitude)
                .LessThanOrEqualTo(Constants.MaxLongitude);

            RuleFor(request => request.To.Latitude)
                .GreaterThanOrEqualTo(Constants.MinLatitude)
                .LessThanOrEqualTo(Constants.MaxLatitude);
            RuleFor(request => request.To.Longitude)
                .GreaterThanOrEqualTo(Constants.MinLongitude)
                .LessThanOrEqualTo(Constants.MaxLongitude);
        }
    }
}
