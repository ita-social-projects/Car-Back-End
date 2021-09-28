using Car.Data.Constants;
using Car.Domain.Dto;
using Car.Domain.Dto.Chat;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CreateChatDtoValidator : AbstractValidator<CreateChatDto>
    {
        public CreateChatDtoValidator()
        {
            RuleFor(chat => chat.Name).NotNull().NotEmpty().MaximumLength(Constants.StringMaxLength);
        }
    }
}