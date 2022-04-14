using Car.Data.Entities;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class UserStopValidator : AbstractValidator<UserStop>
    {
        public UserStopValidator()
        {
            RuleFor(journeyUser => journeyUser.UserId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(journeyUser => journeyUser.StopId).GreaterThan(Constants.Constants.IdLength);
        }
    }
}