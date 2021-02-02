using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class ScheduleValidator : AbstractValidator<Entities.Schedule>
    {
        public ScheduleValidator()
        {
            RuleFor(schedule => schedule.Id).GreaterThan(0);
            RuleFor(schedule => schedule.Name).NotNull().Length(2, 50);
        }
    }
}
