using Car.Data.Constants;
using Car.Data.FluentValidation;
using Car.Domain.Dto.ChatDto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class ChatDtoValidator : AbstractValidator<ChatDto>
    {
        public ChatDtoValidator()
        {
            RuleFor(chat => chat.Name).NotNull().NotEmpty().MaximumLength(Constants.StringMaxLength);
            RuleFor(chat => chat.MessageText).NotNull().NotEmpty().MaximumLength(Constants.StringMaxLength);
        }
    }
}