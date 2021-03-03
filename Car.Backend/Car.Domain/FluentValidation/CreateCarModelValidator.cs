using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CreateCarModelValidator : AbstractValidator<Models.Car.CreateCarModel>
    {
        public CreateCarModelValidator()
        {
            RuleFor(car => car.OwnerId).GreaterThan(Constants.IDLENGTH);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.ModelId).GreaterThan(Constants.IDLENGTH);
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PLATENUMBERMINLENGTH)
                                                     .MaximumLength(Constants.PLATENUMBERMAXLENGTH);
        }
    }
}
