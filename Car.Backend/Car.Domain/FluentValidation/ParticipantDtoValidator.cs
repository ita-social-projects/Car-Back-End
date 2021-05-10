using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class ParticipantDtoValidator : AbstractValidator<Dto.ParticipantDto>
    {
        public ParticipantDtoValidator()
        {
            RuleFor(participant => participant.JourneyId).GreaterThan(Constants.IdLength);
            RuleFor(participant => participant.UserId).GreaterThan(Constants.IdLength);
            RuleFor(participant => participant.HasLuggage).NotNull();
        }
    }
}
