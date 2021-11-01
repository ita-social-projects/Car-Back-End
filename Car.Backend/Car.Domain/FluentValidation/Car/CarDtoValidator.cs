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
            RuleFor(car => car.PlateNumber).NotEmpty()
                                            .MinimumLength(Constants.PlateNumberMinLength)
                                            .MaximumLength(Constants.PlateNumberMaxLength)
                                            .Matches("^[A-Za-zА-ЯҐЄІЇа-яґєії0-9- ]+$")
                                            .When(car => car.PlateNumber != null);
            RuleFor(car => car.OwnerId).GreaterThan(Constants.IdLength);
            RuleFor(car => car.Brand).NotNull().NotEmpty();
            RuleFor(car => car.Model).NotNull().NotEmpty();
        }
    }
}
