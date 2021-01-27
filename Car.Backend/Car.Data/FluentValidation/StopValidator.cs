using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class StopValidator : AbstractValidator<Entities.Stop>
    {
        public StopValidator()
        {
            RuleFor(stop => stop.Id).GreaterThan(0);
            RuleFor(stop => stop.JourneyId).GreaterThan(0);
            RuleFor(stop => stop.UserId).GreaterThan(0);
            RuleFor(stop => stop.AddressId).GreaterThan(0);
            RuleFor(stop => stop.Address).SetValidator(new AddressValidator());
        }
    }
}
