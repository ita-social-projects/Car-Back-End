using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class JourneyValidator : AbstractValidator<Entities.Journey>
    {
        public JourneyValidator()
        {
            RuleFor(journey => journey.Id).GreaterThan(0);
            RuleFor(journey => journey.RouteDistance).NotNull().GreaterThan(0);
            RuleFor(journey => journey.DepartureTime).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(journey => journey.CountOfSeats).NotNull().GreaterThan(0).LessThan(10);
            RuleFor(journey => journey.Comments).Length(0, 100);
            RuleFor(journey => journey.IsFree).NotNull();
            RuleForEach(journey => journey.Participants).SetValidator(new UserJourneyValidator());
            RuleForEach(journey => journey.UserStops).SetValidator(new StopValidator());
            RuleFor(journey => journey.Schedule).SetValidator(new ScheduleValidator());
            RuleFor(journey => journey.Driver).NotNull().SetValidator(new UserValidator());
        }
    }
}
