using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class UserValidator : AbstractValidator<Entities.User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(user => user.Name).NotNull().NotEmpty().MaximumLength(Constants.Constants.StringMaxLength);
            RuleFor(user => user.Surname).NotNull().NotEmpty().MaximumLength(Constants.Constants.StringMaxLength);
            RuleFor(user => user.Position).NotNull().NotEmpty().MaximumLength(Constants.Constants.PositionMaxLength);
            RuleFor(user => user.Location).NotNull().NotEmpty().MaximumLength(Constants.Constants.LocationMaxLength);
            RuleFor(user => user.HireDate).NotNull().LessThanOrEqualTo(DateTime.Now);
            RuleFor(user => user.Email).NotNull().MinimumLength(Constants.Constants.EmailMinLength)
                                                 .MaximumLength(Constants.Constants.EmailMaxLength)
                                                 .EmailAddress();
        }
    }
}
