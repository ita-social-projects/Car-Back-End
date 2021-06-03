using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CreateCarModelValidator : AbstractValidator<CreateCarDto>
    {
        public CreateCarModelValidator()
        {
            RuleFor(car => car.OwnerId).NotNull().GreaterThan(Constants.IdLength);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.ModelId).NotNull().GreaterThan(Constants.IdLength);
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PlateNumberMinLength)
                                                     .MaximumLength(Constants.PlateNumberMaxLength);
        }
    }
}
