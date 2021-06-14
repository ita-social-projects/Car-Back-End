using Car.Data.Constants;
using Car.Data.FluentValidation;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CarDtoValidator : AbstractValidator<Dto.CarDto>
    {
        public CarDtoValidator()
        {
            RuleFor(car => car.Id).GreaterThan(Constants.IdLength);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PlateNumberMinLength)
                                                     .MaximumLength(Constants.PlateNumberMaxLength);
            RuleFor(car => car.OwnerId).GreaterThan(Constants.IdLength);
            RuleFor(car => car.Model).SetValidator(new ModelValidator()!);
        }
    }
}
