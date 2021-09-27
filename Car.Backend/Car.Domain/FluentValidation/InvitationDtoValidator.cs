using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class InvitationDtoValidator : AbstractValidator<Dto.InvitationDto>
    {
        public InvitationDtoValidator()
        {
            RuleFor(invite => invite.InvitedUserId).GreaterThan(Constants.IdLength);
            RuleFor(invite => invite.JourneyId).GreaterThan(Constants.IdLength);
            RuleFor(invite => invite.Type).IsInEnum();
        }
    }
}
