using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateCarDtoValidator : AbstractValidator<UpdateCarDto>
    {
        public UpdateCarDtoValidator()
        {
            RuleFor(car => car.Id).GreaterThan(Constants.IdLength);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.Brand).NotNull().NotEmpty();
            RuleFor(car => car.Model).NotNull().NotEmpty();
            RuleFor(car => car.PlateNumber).NotEmpty()
                                            .MinimumLength(Constants.PlateNumberMinLength)
                                            .MaximumLength(Constants.PlateNumberMaxLength)
                                            .Matches("^[A-Za-zА-ЯҐЄІЇа-яґєії0-9- ]+$")
                                            .When(car => car.PlateNumber != null);
        }
    }
}
