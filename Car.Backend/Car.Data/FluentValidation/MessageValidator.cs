using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class MessageValidator : AbstractValidator<Entities.Message>
    {
        public MessageValidator()
        {
            RuleFor(message => message.Id).GreaterThan(0);
            RuleFor(message => message.Text).NotNull().Length(1, 400);
            RuleFor(message => message.CreatedAt).NotNull().GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(message => message.SenderId).GreaterThan(0);
            RuleFor(message => message.ChatId).GreaterThan(0);
            RuleFor(message => message.Sender).NotNull().SetValidator(new UserValidator());
        }
    }
}
