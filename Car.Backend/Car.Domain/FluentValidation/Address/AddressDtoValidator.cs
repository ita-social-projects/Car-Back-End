using Car.Data.Constants;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class AddressDtoValidator : AbstractValidator<Dto.Address.AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(address => address.Name).NotNull().NotEmpty();
            RuleFor(address => address.Latitude).InclusiveBetween(Constants.MinLatitude, Constants.MaxLatitude);
            RuleFor(address => address.Longitude).InclusiveBetween(Constants.MinLongitude, Constants.MaxLongitude);
        }
    }
}
