using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class UserPreferencesValidator : AbstractValidator<Entities.UserPreferences>
    {
        public UserPreferencesValidator()
        {
            RuleFor(userPreferences => userPreferences.Id).GreaterThan(0);
            RuleFor(userPreferences => userPreferences.DoAllowSmoking).NotNull();
            RuleFor(userPreferences => userPreferences.DoAllowEating).NotNull();
            RuleFor(userPreferences => userPreferences.Comments).Length(0, 100);
        }
    }
}
