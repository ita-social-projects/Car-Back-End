using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateCarModelValidator : AbstractValidator<Models.Car.UpdateCarModel>
    {
        public UpdateCarModelValidator()
        {
            RuleFor(car => car.Id).GreaterThan(Constants.IDLENGTH);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.ModelId).GreaterThan(Constants.IDLENGTH);
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PLATENUMBERMINLENGTH)
                                                     .MaximumLength(Constants.PLATENUMBERMAXLENGTH);
        }
    }
}
