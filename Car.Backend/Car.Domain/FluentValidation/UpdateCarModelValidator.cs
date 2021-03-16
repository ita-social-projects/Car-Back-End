using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateCarModelValidator : AbstractValidator<Models.Car.UpdateCarModel>
    {
        public UpdateCarModelValidator()
        {
            RuleFor(car => car.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.ModelId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PLATENUMBER_MIN_LENGTH)
                                                     .MaximumLength(Constants.PLATENUMBER_MAX_LENGTH);
        }
    }
}
