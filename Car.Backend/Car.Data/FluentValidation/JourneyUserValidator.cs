using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.Entities;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class JourneyUserValidator : AbstractValidator<JourneyUser>
    {
        public JourneyUserValidator()
        {
            RuleFor(journeyUser => journeyUser.UserId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(journeyUser => journeyUser.JourneyId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(journeyUser => journeyUser.WithBaggage).NotNull();
            RuleFor(journeyUser => journeyUser.PassangersCount).GreaterThan(0);
        }
    }
}
