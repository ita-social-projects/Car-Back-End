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
            RuleFor(journey => journey.Id).GreaterThan(Constants.IDLENGTH);
            RuleFor(journey => journey.RouteDistance).NotNull().NotEmpty();
            RuleFor(journey => journey.DepartureTime).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(journey => journey.CountOfSeats).NotNull().NotEmpty().GreaterThanOrEqualTo(Constants.SEATSMAXLENGTH);
            RuleFor(journey => journey.Comments).MaximumLength(Constants.COMMENTSMAXLENGTH);
            RuleFor(journey => journey.IsFree).NotNull();
            RuleFor(journey => journey.Organizer).SetValidator(new UserDtoValidator());
            RuleFor(journey => journey.Car).SetValidator(new CarValidator());
            RuleFor(journey => journey.Schedule).SetValidator(new ScheduleValidator());
            RuleForEach(journey => journey.Participants).SetValidator(new UserDtoValidator());
            RuleForEach(journey => journey.Stops).SetValidator(new StopDtoValidator());
        }
    }
}
