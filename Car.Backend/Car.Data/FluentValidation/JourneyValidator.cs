using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class JourneyValidator : AbstractValidator<Entities.Journey>
    {
        public JourneyValidator()
        {
            RuleFor(journey => journey.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(journey => journey.RouteDistance).NotNull().GreaterThan(0);
            RuleFor(journey => journey.DepartureTime).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(journey => journey.CountOfSeats).NotNull().GreaterThan(0).LessThanOrEqualTo(Constants.SEATS_MAX_LENGTH);
            RuleFor(journey => journey.Comments).MaximumLength(Constants.COMMENTS_MAX_LENGTH);
            RuleFor(journey => journey.IsFree).NotNull();
            RuleFor(journey => journey.OrganizerId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(journey => journey.Car).NotNull().SetValidator(new CarValidator());
            RuleFor(journey => journey.Schedule).SetValidator(new ScheduleValidator());
            RuleFor(journey => journey.Organizer).NotNull().SetValidator(new UserValidator());
            RuleForEach(journey => journey.Participants).SetValidator(new UserValidator());
            RuleForEach(journey => journey.Stops).SetValidator(new StopValidator());
        }
    }
}
