using Car.Data.Constants;
using Car.Domain.Models.User;
using FluentValidation;

namespace Car.Domain.FluentValidation.Journey
{
    public class JourneyUserModelValidator : AbstractValidator<JourneyUserModel>
    {
        public JourneyUserModelValidator()
        {
            RuleFor(x => x.JourneyId).GreaterThan(Constants.IdLength);
            RuleFor(x => x.UserId).GreaterThan(Constants.IdLength);
            RuleFor(x => x.PassangersCount)
                .GreaterThanOrEqualTo(Constants.SeatsMinCount)
                .LessThanOrEqualTo(Constants.SeatsMaxCount);
        }
    }
}
