using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class ModelValidator : AbstractValidator<Entities.Model>
    {
        public ModelValidator()
        {
            RuleFor(model => model.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(model => model.BrandId).GreaterThan(Constants.Constants.IdLength);
            RuleFor(model => model.Name).NotNull().NotEmpty().MaximumLength(Constants.Constants.StringMaxLength);
            RuleFor(model => model.Brand).SetValidator(new BrandValidator());
        }
    }
}
