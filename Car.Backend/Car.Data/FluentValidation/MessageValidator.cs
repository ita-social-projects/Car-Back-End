using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class MessageValidator : AbstractValidator<Entities.Message>
    {
        public MessageValidator()
        {
            RuleFor(message => message.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(message => message.Text).NotNull().NotEmpty().MaximumLength(Constants.Constants.TextMaxLength);
            RuleFor(message => message.CreatedAt).NotNull().GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(message => message.SenderId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(message => message.ChatId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(message => message.Sender).NotNull().SetValidator(new UserValidator());
        }
    }
}
