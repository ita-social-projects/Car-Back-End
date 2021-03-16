using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class CarValidator : AbstractValidator<Entities.Car>
    {
        public CarValidator()
        {
            RuleFor(car => car.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PLATENUMBER_MIN_LENGTH)
                                                     .MaximumLength(Constants.PLATENUMBER_MAX_LENGTH);
            RuleFor(car => car.OwnerId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(car => car.Owner).NotNull().SetValidator(new UserValidator());
            RuleFor(car => car.Model).NotNull().SetValidator(new ModelValidator());
        }
    }
}
