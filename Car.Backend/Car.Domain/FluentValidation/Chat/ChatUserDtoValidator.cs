using Car.Data.Constants;
using Car.Domain.Dto;
using Car.Domain.Dto.Chat;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class ChatUserDtoValidator : AbstractValidator<ChatUserDto>
    {
        public ChatUserDtoValidator()
        {
            RuleFor(user => user.Name).NotNull().NotEmpty().MaximumLength(Constants.StringMaxLength);
            RuleFor(user => user.Surname).NotNull().NotEmpty().MaximumLength(Constants.StringMaxLength);
        }
    }
}
