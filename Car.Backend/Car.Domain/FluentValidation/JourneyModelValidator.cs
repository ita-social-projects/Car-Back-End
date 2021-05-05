using System;
using Car.Data;
using Car.Data.FluentValidation;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class JourneyModelValidator : AbstractValidator<Models.Journey.JourneyModel>
    {
        public JourneyModelValidator()
        {
            RuleFor(journey => journey.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(journey => journey.RouteDistance).NotNull().NotEmpty().GreaterThan(Constants.NUMBER_MIN);
            RuleFor(journey => journey.DepartureTime).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(journey => journey.CountOfSeats).NotNull().NotEmpty().GreaterThan(Constants.NUMBER_MIN).LessThanOrEqualTo(Constants.SEATS_MAX_COUNT);
            RuleFor(journey => journey.Comments).MaximumLength(Constants.COMMENTS_MAX_LENGTH);
            RuleFor(journey => journey.IsFree).NotNull();
            RuleFor(journey => journey.IsOnOwnCar).NotNull();
            RuleFor(journey => journey.Organizer).SetValidator(new UserDtoValidator());
            RuleFor(journey => journey.Car).SetValidator(new CarValidator());
            RuleFor(journey => journey.Schedule).SetValidator(new ScheduleValidator());
            RuleForEach(journey => journey.Participants).SetValidator(new UserDtoValidator());
            RuleForEach(journey => journey.Stops).SetValidator(new StopDtoValidator());
        }
    }
}
