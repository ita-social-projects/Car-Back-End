using Car.Data;
using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class CreateLocationModelValidator : AbstractValidator<Models.Location.CreateLocationModel>
    {
        public CreateLocationModelValidator()
        {
            RuleFor(location => location.Address).SetValidator(new AddressDtoValidator());
            RuleFor(location => location.Name).MaximumLength(Constants.LocationNameMaxLength);
            RuleFor(location => location.TypeId).GreaterThan(Constants.IdLength);
            RuleFor(location => location.UserId).GreaterThan(Constants.IdLength);
        }
    }
}
