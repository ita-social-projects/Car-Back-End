using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class BrandValidator : AbstractValidator<Entities.Brand>
    {
        public BrandValidator()
        {
            RuleFor(brand => brand.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(brand => brand.Name).NotNull().NotEmpty().MaximumLength(Constants.STRING_MAX_LENGTH);
        }
    }
}
