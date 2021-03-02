using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidationDto
{
    public class ParticipantDtoValidator : AbstractValidator<Dto.ParticipantDto>
    {
        public ParticipantDtoValidator()
        {
            RuleFor(participant => participant.JourneyId).GreaterThan(Constants.IDLENGTH);
            RuleFor(participant => participant.UserId).GreaterThan(Constants.IDLENGTH);
            RuleFor(participant => participant.HasLuggage).NotNull();
        }
    }
}
