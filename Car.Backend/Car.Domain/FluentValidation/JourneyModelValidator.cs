using System;
using Car.Data.Constants;
using Car.Data.FluentValidation;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class JourneyModelValidator : AbstractValidator<Models.Journey.JourneyModel>
    {
        public JourneyModelValidator()
        {
            RuleFor(journey => journey.Id).GreaterThan(Constants.IdLength);
            RuleFor(journey => journey.RouteDistance).NotNull().NotEmpty().GreaterThan(Constants.NumberMin);
            RuleFor(journey => journey.DepartureTime).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(journey => journey.CountOfSeats).NotNull().NotEmpty().GreaterThan(Constants.NumberMin).LessThanOrEqualTo(Constants.SeatsMaxCount);
            RuleFor(journey => journey.Comments).MaximumLength(Constants.CommentsMaxLength);
            RuleFor(journey => journey.IsFree).NotNull();
            RuleFor(journey => journey.IsOnOwnCar).NotNull();
            RuleFor(journey => journey.Organizer).SetValidator(new UserDtoValidator()!);
            RuleFor(journey => journey.Car).SetValidator(new CarValidator()!);
            RuleFor(journey => journey.Schedule).SetValidator(new ScheduleValidator()!);
            RuleForEach(journey => journey.Participants).SetValidator(new UserDtoValidator());
            RuleForEach(journey => journey.Stops).SetValidator(new StopDtoValidator());
        }
    }
}
