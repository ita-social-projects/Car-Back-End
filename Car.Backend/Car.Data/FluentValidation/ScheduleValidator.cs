using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class ScheduleValidator : AbstractValidator<Entities.Schedule>
    {
        public ScheduleValidator()
        {
            RuleFor(schedule => schedule.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(schedule => schedule.Name).NotNull().NotEmpty().MaximumLength(Constants.Constants.StringMaxLength);
        }
    }
}
