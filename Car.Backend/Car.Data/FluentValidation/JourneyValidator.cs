using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class JourneyValidator : AbstractValidator<Entities.Journey>
    {
        public JourneyValidator()
        {
            RuleFor(journey => journey.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(journey => journey.RouteDistance).NotNull().GreaterThan(Constants.Constants.NumberMin);
            RuleFor(journey => journey.DepartureTime).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(journey => journey.CountOfSeats).NotNull().GreaterThan(Constants.Constants.NumberMin).LessThanOrEqualTo(Constants.Constants.SeatsMaxCount);
            RuleFor(journey => journey.Comments).MaximumLength(Constants.Constants.CommentsMaxLength);
            RuleFor(journey => journey.IsFree).NotNull();
            RuleFor(journey => journey.IsOnOwnCar).NotNull();
            RuleFor(journey => journey.OrganizerId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(journey => journey.Car).NotNull().SetValidator(new CarValidator()!);
            RuleFor(journey => journey.Schedule).SetValidator(new ScheduleValidator()!);
            RuleFor(journey => journey.Organizer).NotNull().SetValidator(new UserValidator()!);
            RuleForEach(journey => journey.Participants).SetValidator(new UserValidator());
            RuleForEach(journey => journey.Stops).SetValidator(new StopValidator());
        }
    }
}
