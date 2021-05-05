using Car.Data;
using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class UpdateCarModelValidator : AbstractValidator<Models.Car.UpdateCarModel>
    {
        public UpdateCarModelValidator()
        {
            RuleFor(car => car.Id).GreaterThan(Constants.IdLength);
            RuleFor(car => car.Color).NotNull();
            RuleFor(car => car.ModelId).GreaterThan(Constants.IdLength);
            RuleFor(car => car.PlateNumber).NotNull().NotEmpty()
                                                     .MinimumLength(Constants.PlateNumberMinLength)
                                                     .MaximumLength(Constants.PlateNumberMaxLength);
        }
    }
}
