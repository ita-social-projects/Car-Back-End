using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UserPreferencesDtoValidator : AbstractValidator<UserPreferencesDto>
    {
        public UserPreferencesDtoValidator()
        {
            RuleFor(userPreferences => userPreferences.Id).GreaterThan(Constants.IdLength);
            RuleFor(userPreferences => userPreferences.DoAllowSmoking).NotNull();
            RuleFor(userPreferences => userPreferences.DoAllowEating).NotNull();
            RuleFor(userPreferences => userPreferences.Comments).MaximumLength(Constants.CommentsMaxLength);
        }
    }
}
