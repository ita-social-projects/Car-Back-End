using System;
using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UserDtoValidator : AbstractValidator<Dto.UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Id).GreaterThan(Constants.IDLENGTH);
            RuleFor(user => user.Name).NotNull().NotEmpty().MaximumLength(Constants.STRINGMAXLENGTH);
            RuleFor(user => user.Surname).NotNull().NotEmpty().MaximumLength(Constants.STRINGMAXLENGTH);
            RuleFor(user => user.Position).NotNull().NotEmpty().MaximumLength(Constants.STRINGMAXLENGTH);
            RuleFor(user => user.Location).NotNull().NotEmpty().MaximumLength(Constants.STRINGMAXLENGTH);
            RuleFor(user => user.HireDate).NotNull().LessThanOrEqualTo(DateTime.Now);
            RuleFor(user => user.Email).NotNull().MinimumLength(Constants.EMAILMINLENGTH)
                                                 .MaximumLength(Constants.EMAILMAXLENGTH)
                                                 .EmailAddress();
        }
    }
}
