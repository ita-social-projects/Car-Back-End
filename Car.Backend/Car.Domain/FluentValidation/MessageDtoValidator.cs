using System;
using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class MessageDtoValidator : AbstractValidator<MessageDto>
    {
        public MessageDtoValidator()
        {
            RuleFor(message => message.ChatId).GreaterThan(Constants.IdLength);
            RuleFor(message => message.Text).NotNull().NotEmpty().MaximumLength(Constants.TextMaxLength);
            RuleFor(message => message.SenderId).GreaterThan(Constants.IdLength);
            RuleFor(message => message.SenderName).NotNull().NotEmpty().MaximumLength(Constants.StringMaxLength);
            RuleFor(message => message.SenderSurname).NotNull().NotEmpty().MaximumLength(Constants.StringMaxLength);
        }
    }
}
