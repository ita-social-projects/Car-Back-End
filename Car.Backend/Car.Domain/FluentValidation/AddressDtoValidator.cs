using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class AddressDtoValidator : AbstractValidator<Dto.AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(address => address.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(address => address.City).NotNull().NotEmpty().MaximumLength(Constants.STRING_MAX_LENGTH);
            RuleFor(address => address.Street).NotNull().NotEmpty().MaximumLength(Constants.STRING_MAX_LENGTH);
            RuleFor(address => address.Latitude).NotNull();
            RuleFor(address => address.Longitude).NotNull();
        }
    }
}
