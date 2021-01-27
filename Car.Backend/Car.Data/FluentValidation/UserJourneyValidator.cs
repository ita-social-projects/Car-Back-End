using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class UserJourneyValidator : AbstractValidator<Entities.UserJourney>
    {
        public UserJourneyValidator()
        {
            RuleFor(userJourney => userJourney.UserId).GreaterThan(0);
            RuleFor(userJourney => userJourney.JourneyId).GreaterThan(0);
        }
    }
}
