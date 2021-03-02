using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class ModelValidator : AbstractValidator<Entities.Model>
    {
        public ModelValidator()
        {
            RuleFor(model => model.Id).GreaterThan(Constants.IDLENGTH);
            RuleFor(model => model.BrandId).GreaterThan(Constants.IDLENGTH);
            RuleFor(model => model.Name).NotNull().NotEmpty().MaximumLength(Constants.STRINGMAXLENGTH);
            RuleFor(model => model.Brand).SetValidator(new BrandValidator());
        }
    }
}
