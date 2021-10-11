using System;
using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class JourneyDtoValidator : AbstractValidator<JourneyDto>
    {
        public JourneyDtoValidator()
        {
            When(model => model.WeekDay == null, () =>
                RuleFor(model => model.DepartureTime).GreaterThan(DateTime.UtcNow));
            RuleFor(model => model.CountOfSeats)
                .GreaterThan(Constants.NumberMin)
                .LessThanOrEqualTo(Constants.SeatsMaxCount);
            RuleFor(model => model.Comments).MaximumLength(Constants.CommentsMaxLength);
            RuleFor(model => model.IsFree).NotNull();
            RuleFor(model => model.OrganizerId).GreaterThan(Constants.IdLength);
            RuleFor(model => model.IsOnOwnCar).NotNull();
            RuleForEach(model => model.JourneyPoints).SetValidator(new JourneyPointDtoValidator());
            RuleForEach(model => model.Stops).SetValidator(new StopDtoValidator());
            RuleFor(model => model.RouteDistance).GreaterThan(Constants.NumberMin);
            RuleFor(model => model.Duration).GreaterThan(TimeSpan.Zero);
        }
    }
}