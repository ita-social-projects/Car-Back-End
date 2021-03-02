using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class MessageValidator : AbstractValidator<Entities.Message>
    {
        public MessageValidator()
        {
            RuleFor(message => message.Id).GreaterThan(Constants.IDLENGTH);
            RuleFor(message => message.Text).NotNull().NotEmpty().MaximumLength(Constants.TEXTMAXLENGTH);
            RuleFor(message => message.CreatedAt).NotNull().GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(message => message.SenderId).GreaterThan(Constants.IDLENGTH);
            RuleFor(message => message.ChatId).GreaterThan(Constants.IDLENGTH);
            RuleFor(message => message.Sender).NotNull().SetValidator(new UserValidator());
        }
    }
}
