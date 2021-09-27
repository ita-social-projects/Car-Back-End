using System;
using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UserFcmTokenDtoValidator : AbstractValidator<UserFcmTokenDto>
    {
        public UserFcmTokenDtoValidator()
        {
            RuleFor(user => user.Token).NotNull().NotEmpty();
        }
    }
}