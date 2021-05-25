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
            RuleFor(request => request.PassengersCount)
                .GreaterThan(Constants.Constants.NumberMin)
                .LessThanOrEqualTo(Constants.Constants.SeatsMaxCount);
            RuleFor(request => request.UserId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(request => request.User).NotNull().SetValidator(new UserValidator());
            RuleFor(request => request.Fee).IsInEnum();
            RuleFor(request => request.From).NotNull().SetValidator(new PointValidator());
            RuleFor(request => request.To).NotNull().SetValidator(new PointValidator());
        }
    }
}
