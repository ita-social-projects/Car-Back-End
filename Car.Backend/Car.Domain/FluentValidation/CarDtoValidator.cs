using Car.Data;
using Car.Data.FluentValidation;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CarDtoValidator : AbstractValidator<Dto.CarDto>
    {
        public CarDtoValidator()
        {
            RuleFor(car => car.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PLATENUMBER_MIN_LENGTH)
                                                     .MaximumLength(Constants.PLATENUMBER_MAX_LENGTH);
            RuleFor(car => car.OwnerId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(car => car.Model).SetValidator(new ModelValidator());
        }
    }
}
