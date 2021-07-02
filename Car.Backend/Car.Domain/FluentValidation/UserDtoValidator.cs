using System;
using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Id).GreaterThan(Constants.IdLength);
            RuleFor(user => user.Name).NotNull().NotEmpty().MaximumLength(Constants.StringMaxLength);
            RuleFor(user => user.Surname).NotNull().NotEmpty().MaximumLength(Constants.StringMaxLength);
            RuleFor(user => user.Position).NotNull().NotEmpty().MaximumLength(Constants.PositionMaxLength);
            RuleFor(user => user.Location).NotNull().NotEmpty().MaximumLength(Constants.LocationMaxLength);
            RuleFor(user => user.HireDate).NotNull().LessThanOrEqualTo(DateTime.Now);
            RuleFor(user => user.Email).NotNull().MinimumLength(Constants.EmailMinLength)
                                                 .MaximumLength(Constants.EmailMaxLength)
                                                 .EmailAddress();
        }
    }
}
