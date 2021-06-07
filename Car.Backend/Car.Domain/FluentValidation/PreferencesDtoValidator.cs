using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class PreferencesDtoValidator : AbstractValidator<Dto.UserPreferencesDto>
    {
        public PreferencesDtoValidator()
        {
            RuleFor(userPreferences => userPreferences.Id).GreaterThan(Constants.IdLength);
            RuleFor(userPreferences => userPreferences.DoAllowSmoking).NotNull();
            RuleFor(userPreferences => userPreferences.DoAllowEating).NotNull();
            RuleFor(userPreferences => userPreferences.Comments).MaximumLength(Constants.CommentsMaxLength);
        }
    }
}
