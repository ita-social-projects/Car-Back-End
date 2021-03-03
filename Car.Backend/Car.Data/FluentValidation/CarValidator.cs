using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class CarValidator : AbstractValidator<Entities.Car>
    {
        public CarValidator()
        {
            RuleFor(car => car.Id).GreaterThan(0);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.PlateNumber).NotNull().Length(4, 10);
            RuleFor(car => car.OwnerId).GreaterThan(0);
            RuleFor(car => car.Owner).SetValidator(new UserValidator());
        }
    }
}
