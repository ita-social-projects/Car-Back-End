using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class BrandDtoValidator : AbstractValidator<Dto.BrandDto>
    {
        public BrandDtoValidator()
        {
            RuleFor(brand => brand.Id).GreaterThan(Constants.IdLength);
            RuleFor(brand => brand.Name).NotNull().NotEmpty().MaximumLength(Constants.StringMaxLength);
        }
    }
}
