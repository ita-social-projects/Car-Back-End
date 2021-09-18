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
            RuleFor(car => car.ModelId).GreaterThan(Constants.IdLength);
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PlateNumberMinLength)
                                                     .MaximumLength(Constants.PlateNumberMaxLength)
                                                     .Matches("^[A-Za-zА-ЯҐЄІЇа-яґєії0-9- ]+$");
        }
    }
}
