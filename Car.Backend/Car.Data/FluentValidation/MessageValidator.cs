using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class MessageValidator : AbstractValidator<Entities.Message>
    {
        public MessageValidator()
        {
            RuleFor(message => message.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(message => message.Text).NotNull().NotEmpty().MaximumLength(Constants.TEXT_MAX_LENGTH);
            RuleFor(message => message.CreatedAt).NotNull().GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(message => message.SenderId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(message => message.ChatId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(message => message.Sender).NotNull().SetValidator(new UserValidator());
        }
    }
}
