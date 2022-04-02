using Car.Data.Constants;
using Car.Domain.Models.Stops;
using FluentValidation;

namespace Car.Domain.FluentValidation.Address
{
    public class AddressModelValidator : AbstractValidator<AddressModel>
    {
        public AddressModelValidator()
        {
            RuleFor(address => address.Name).NotEmpty();
            RuleFor(address => address.Latitude).InclusiveBetween(Constants.MinLatitude, Constants.MaxLatitude);
            RuleFor(address => address.Longitude).InclusiveBetween(Constants.MinLongitude, Constants.MaxLongitude);
        }
    }
}
