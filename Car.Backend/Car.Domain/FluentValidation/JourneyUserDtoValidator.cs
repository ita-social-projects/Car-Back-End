using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class JourneyUserDtoValidator : AbstractValidator<Dto.JourneyUserDto>
    {
        public JourneyUserDtoValidator()
        {
            RuleFor(participant => participant.JourneyId).GreaterThan(Constants.IdLength);
            RuleFor(participant => participant.UserId).GreaterThan(Constants.IdLength);
            RuleFor(participant => participant.WithBaggage).NotNull();
            RuleFor(participant => participant.PassangersCount).GreaterThan(0);
            RuleFor(participant => participant.PassangersCount).LessThan(5);
        }
    }
}
