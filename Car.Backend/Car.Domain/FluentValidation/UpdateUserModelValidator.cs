using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateUserModelValidator : AbstractValidator<Models.User.UpdateUserModel>
    {
        public UpdateUserModelValidator()
        {
            RuleFor(user => user.Id).GreaterThan(Constants.IdLength);
        }
    }
}
