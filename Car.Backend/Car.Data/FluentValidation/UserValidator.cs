using System;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class UserValidator : AbstractValidator<Entities.User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id).GreaterThan(0);
            RuleFor(user => user.Name).NotNull().Length(1, 64);
            RuleFor(user => user.Surname).NotNull().Length(1, 64);
            RuleFor(user => user.Position).NotNull().Length(1, 100);
            RuleFor(user => user.Location).NotNull().Length(1, 100);
            RuleFor(user => user.HireDate).NotNull().LessThanOrEqualTo(DateTime.Now);
            RuleFor(user => user.Email).NotNull().Length(2, 100).EmailAddress();
        }
    }
}
