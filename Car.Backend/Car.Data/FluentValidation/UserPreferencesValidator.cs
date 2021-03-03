using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class UserPreferencesValidator : AbstractValidator<Entities.UserPreferences>
    {
        public UserPreferencesValidator()
        {
            RuleFor(userPreferences => userPreferences.Id).GreaterThan(Constants.IDLENGTH);
            RuleFor(userPreferences => userPreferences.DoAllowSmoking).NotNull();
            RuleFor(userPreferences => userPreferences.DoAllowEating).NotNull();
            RuleFor(userPreferences => userPreferences.Comments).MaximumLength(Constants.COMMENTSMAXLENGTH);
            RuleFor(userPreferences => userPreferences.User).SetValidator(new UserValidator());
        }
    }
}
