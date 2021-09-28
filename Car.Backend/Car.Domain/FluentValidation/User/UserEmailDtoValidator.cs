using System;
using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UserEmailDtoValidator : AbstractValidator<UserEmailDto>
    {
        public UserEmailDtoValidator()
        {
            RuleFor(user => user.Email).NotNull().MinimumLength(Constants.EmailMinLength)
                .MaximumLength(Constants.EmailMaxLength)
                .EmailAddress();
        }
    }
}
