using Car.Data.Constants;
using Car.Domain.Models.User;
using FluentValidation;

namespace Car.Domain.FluentValidation.Journey
{
    public class JourneyUserModelValidator : AbstractValidator<JourneyUserModel>
    {
        public JourneyUserModelValidator()
        {
            RuleFor(x => x.JourneyId).LessThan(Constants.IdLength);
            RuleFor(x => x.UserId).LessThan(Constants.IdLength);
            RuleFor(x => x.PassangersCount)
                .LessThan(Constants.SeatsMinCount)
                .GreaterThan(Constants.SeatsMaxCount);
        }
    }
}
