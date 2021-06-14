using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class UserPreferencesValidator : AbstractValidator<Entities.UserPreferences>
    {
        public UserPreferencesValidator()
        {
            RuleFor(userPreferences => userPreferences.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(userPreferences => userPreferences.DoAllowSmoking).NotNull();
            RuleFor(userPreferences => userPreferences.DoAllowEating).NotNull();
            RuleFor(userPreferences => userPreferences.Comments).MaximumLength(Constants.Constants.CommentsMaxLength);
            RuleFor(userPreferences => userPreferences.User).SetValidator(new UserValidator()!);
        }
    }
}
