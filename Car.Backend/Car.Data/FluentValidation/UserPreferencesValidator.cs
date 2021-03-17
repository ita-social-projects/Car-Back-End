using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class UserPreferencesValidator : AbstractValidator<Entities.UserPreferences>
    {
        public UserPreferencesValidator()
        {
            RuleFor(userPreferences => userPreferences.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(userPreferences => userPreferences.DoAllowSmoking).NotNull();
            RuleFor(userPreferences => userPreferences.DoAllowEating).NotNull();
            RuleFor(userPreferences => userPreferences.Comments).MaximumLength(Constants.COMMENTS_MAX_LENGTH);
            RuleFor(userPreferences => userPreferences.User).SetValidator(new UserValidator());
        }
    }
}
