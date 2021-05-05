using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class ChatValidator : AbstractValidator<Entities.Chat>
    {
        public ChatValidator()
        {
            RuleFor(chat => chat.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(chat => chat.Name).NotNull().NotEmpty().MaximumLength(Constants.Constants.StringMaxLength);
            RuleFor(chat => chat.Journey).NotNull().SetValidator(new JourneyValidator());
            RuleForEach(chat => chat.Messages).SetValidator(new MessageValidator());
        }
    }
}
