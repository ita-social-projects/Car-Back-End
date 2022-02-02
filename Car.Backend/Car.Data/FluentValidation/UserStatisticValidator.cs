using Car.Data.Entities;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class UserStatisticValidator : AbstractValidator<UserStatistic>
    {
        public UserStatisticValidator()
        {
            RuleFor(statistic => statistic.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(statistic => statistic.TotalKm).GreaterThanOrEqualTo(Constants.Constants.NumberMin);
            RuleFor(statistic => statistic.PassangerJourneysAmount).GreaterThanOrEqualTo(Constants.Constants.NumberMin);
            RuleFor(statistic => statistic.DriverJourneysAmount).GreaterThanOrEqualTo(Constants.Constants.NumberMin);
            RuleFor(statistic => statistic.User).SetValidator(new UserValidator()!);
        }
    }
}