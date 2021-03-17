using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class ModelValidator : AbstractValidator<Entities.Model>
    {
        public ModelValidator()
        {
            RuleFor(model => model.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(model => model.BrandId).GreaterThan(Constants.ID_LENGTH);
            RuleFor(model => model.Name).NotNull().NotEmpty().MaximumLength(Constants.STRING_MAX_LENGTH);
            RuleFor(model => model.Brand).SetValidator(new BrandValidator());
        }
    }
}
