using Car.Data;
using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CreateCarModelValidator : AbstractValidator<Models.Car.CreateCarModel>
    {
        public CreateCarModelValidator()
        {
            RuleFor(car => car.OwnerId).GreaterThan(Constants.IdLength);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.ModelId).GreaterThan(Constants.IdLength);
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PlateNumberMinLength)
                                                     .MaximumLength(Constants.PlateNumberMaxLength);
        }
    }
}
