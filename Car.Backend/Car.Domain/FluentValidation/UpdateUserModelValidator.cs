using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateUserModelValidator : AbstractValidator<Models.User.UpdateUserModel>
    {
        public UpdateUserModelValidator()
        {
            RuleFor(user => user.Id).GreaterThan(Constants.IDLENGTH);
            RuleFor(user => user.Name).NotNull().NotEmpty().MaximumLength(Constants.STRINGMAXLENGTH);
            RuleFor(user => user.Surname).NotNull().NotEmpty().MaximumLength(Constants.STRINGMAXLENGTH);
            RuleFor(user => user.Position).NotNull().NotEmpty().MaximumLength(Constants.STRINGMAXLENGTH);
            RuleFor(user => user.Location).NotNull().NotEmpty().MaximumLength(Constants.STRINGMAXLENGTH);
        }
    }
}
