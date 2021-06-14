using System;
using Car.Data.Constants;
using Car.Data.FluentValidation;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class RequestDtoValidator : AbstractValidator<RequestDto>
    {
        public RequestDtoValidator()
        {
            RuleFor(request => request.DepartureTime).GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(request => request.PassengersCount)
                .GreaterThan(Constants.NumberMin)
                .LessThanOrEqualTo(Constants.SeatsMaxCount);
            RuleFor(request => request.UserId).GreaterThan(Constants.IdLength);
            RuleFor(request => request.Fee).IsInEnum();
            RuleFor(request => request.From).NotNull().SetValidator(new PointValidator()!);
            RuleFor(request => request.To).NotNull().SetValidator(new PointValidator()!);
        }
    }
}
