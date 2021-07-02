using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class ModelDtoValidator : AbstractValidator<Dto.ModelDto>
    {
        public ModelDtoValidator()
        {
            RuleFor(model => model.Id).GreaterThan(Constants.IdLength);
            RuleFor(model => model.BrandId).GreaterThan(Constants.IdLength);
            RuleFor(model => model.Name).NotNull().NotEmpty().MaximumLength(Constants.StringMaxLength);
            RuleFor(model => model.Brand).SetValidator(new BrandDtoValidator()!);
        }
    }
}
