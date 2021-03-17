using System;
using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UserDtoValidator : AbstractValidator<Dto.UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(user => user.Name).NotNull().NotEmpty().MaximumLength(Constants.STRING_MAX_LENGTH);
            RuleFor(user => user.Surname).NotNull().NotEmpty().MaximumLength(Constants.STRING_MAX_LENGTH);
            RuleFor(user => user.Position).NotNull().NotEmpty().MaximumLength(Constants.POSITION_MAX_LENGTH);
            RuleFor(user => user.Location).NotNull().NotEmpty().MaximumLength(Constants.LOCATION_MAX_LENGTH);
            RuleFor(user => user.HireDate).NotNull().LessThanOrEqualTo(DateTime.Now);
            RuleFor(user => user.Email).NotNull().MinimumLength(Constants.EMAIL_MIN_LENGTH)
                                                 .MaximumLength(Constants.EMAIL_MAX_LENGTH)
                                                 .EmailAddress();
        }
    }
}
