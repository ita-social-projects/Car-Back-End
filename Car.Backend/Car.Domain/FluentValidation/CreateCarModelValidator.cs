using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CreateCarModelValidator : AbstractValidator<Models.Car.CreateCarModel>
    {
        public CreateCarModelValidator()
        {
            RuleFor(car => car.OwnerId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.ModelId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PLATENUMBER_MIN_LENGTH)
                                                     .MaximumLength(Constants.PLATENUMBER_MAX_LENGTH);
        }
    }
}
