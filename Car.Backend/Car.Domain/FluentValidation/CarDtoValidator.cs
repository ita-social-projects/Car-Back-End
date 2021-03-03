using Car.Data;
using Car.Data.FluentValidation;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CarDtoValidator : AbstractValidator<Dto.CarDto>
    {
        public CarDtoValidator()
        {
            RuleFor(car => car.Id).GreaterThan(Constants.IDLENGTH);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PLATENUMBERMINLENGTH)
                                                     .MaximumLength(Constants.PLATENUMBERMAXLENGTH);
            RuleFor(car => car.OwnerId).GreaterThan(Constants.IDLENGTH);
            RuleFor(car => car.Model).SetValidator(new ModelValidator());
        }
    }
}
