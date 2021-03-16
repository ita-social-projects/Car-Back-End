using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class ParticipantDtoValidator : AbstractValidator<Dto.ParticipantDto>
    {
        public ParticipantDtoValidator()
        {
            RuleFor(participant => participant.JourneyId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(participant => participant.UserId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(participant => participant.HasLuggage).NotNull();
        }
    }
}
