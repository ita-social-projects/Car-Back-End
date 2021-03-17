using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class ScheduleValidator : AbstractValidator<Entities.Schedule>
    {
        public ScheduleValidator()
        {
            RuleFor(schedule => schedule.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(schedule => schedule.Name).NotNull().NotEmpty().MaximumLength(Constants.STRING_MAX_LENGTH);
        }
    }
}
